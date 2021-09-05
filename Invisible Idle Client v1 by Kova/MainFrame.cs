using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICv2
{
    public partial class FormMain : Form
    {
        private int ActiveProxyTunnel = 0;
        private List<string> names = new List<string>();
        private static List<User> clients = new List<User>();
        public static List<string> Banned = new List<string>();
        public static int SteamReject = 0;
        public static int InvalidPass = 0;
        internal static List<User> Clients { get => clients; set => clients = value; }
        private bool isRunning = false;
        public FormMain()
        {
            InitializeComponent();
            Connections.MouseClick += Connections_MouseClick;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.SplitterDistance = 510;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Title.Text = this.Text;
            NameText.Text = "IICv2";
            steamid.SelectedIndex = 2;

            if (File.Exists("names.txt"))
            {
                using (StreamReader sr = new StreamReader("names.txt", true))
                {
                    while (!sr.EndOfStream)
                        names.Add(sr.ReadLine());
                }
                if (names.Count() >= 40)
                    cb_NameText.Checked = false;
            }

            if (File.Exists("proxys.txt"))
            {
                using (StreamReader sr = new StreamReader("proxys.txt", true))
                {
                    string list = string.Empty;
                    while (!sr.EndOfStream)
                        list += sr.ReadLine() + "\n";
                    ProxyList.Text = list;

                }
                cb_Proxy.Checked = true;
            }
        }

        #region NumberKeyHandle
        private void KeyPressNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void MaxConnect_TextChanged(object sender, EventArgs e)
        {
            int Num = 1;
            int.TryParse(MaxConnect.Text, out Num);
            if (Num > 32)
                MaxConnect.Text = "32";
            else if(Num < 1)
                MaxConnect.Text = "1";
        }

        private void RefreshRate_TextChanged(object sender, EventArgs e)
        {
            int Num = 0;
            int.TryParse(RefreshRate.Text, out Num);
            if (Num > 60000)
                RefreshRate.Text = "60000";
            else if (Num < 1)
                RefreshRate.Text = "0";
            RefreshRate.SelectionStart = RefreshRate.Text.Length;
            RefreshRate.SelectionLength = 0;
        }

        private void NameText_TextChanged(object sender, EventArgs e)
        {
            if (NameText.Text.Length > 25)
            {
                NameText.Text = NameText.Text.Substring(0, 25);
                NameText.SelectionStart = NameText.Text.Length;
                NameText.SelectionLength = 0;
            }
        }

        private void tb_Threads_TextChanged(object sender, EventArgs e)
        {
            int Num = 0;
            int.TryParse(tb_Threads.Text, out Num);
            if (Num > 1000)
                tb_Threads.Text = "1000";
            else if (Num < 1)
                tb_Threads.Text = "1";
            tb_Threads.SelectionStart = tb_Threads.Text.Length;
            tb_Threads.SelectionLength = 0;
        }

        private void tb_Timeout_TextChanged(object sender, EventArgs e)
        {
            int Num = 0;
            int.TryParse(tb_Timeout.Text, out Num);
            if (Num > 10000)
                tb_Timeout.Text = "10000";
            else if (Num < 50)
                tb_Timeout.Text = "50";
            tb_Timeout.SelectionStart = tb_Timeout.Text.Length;
            tb_Timeout.SelectionLength = 0;
        }
        #endregion

        #region SafeChange
        delegate void SetTextCallback(string text);
        delegate int RowsAddCallback(params object[] values);
        delegate void RowsEditCallback(Guid indenty, int cellIndex, string s);
        delegate void RowDeleteCallback(Guid indenty);
        delegate void RowChangeColorCallback(Guid indenty, Color color);
        delegate void PrintCCallback(string text, Color color, bool newline = true);
        delegate void Callback();

        public void PrintC(string text, Color color, bool newline = true)
        {
            if (this.rtb_Console.InvokeRequired)
            {
                PrintCCallback d = new PrintCCallback(PrintC);
                this.BeginInvoke(d, new object[] { text, color, newline });
            }
            else
            {
                rtb_Console.SelectionFont = new Font("Consolas", 10, FontStyle.Bold);
                rtb_Console.SelectionColor = color;
                rtb_Console.SelectedText = text + (newline ? ";\r\n" : null);
            }
        }
        public void RowChangeColor(Guid indenty, Color color)
        {
            if (this.Connections.InvokeRequired)
            {
                RowChangeColorCallback d = new RowChangeColorCallback(RowChangeColor);
                this.BeginInvoke(d, new object[] { indenty, color });
            }
            else
            {
                for (int i = 0; i < this.Connections.Rows.Count; i++)
                {
                    if (Guid.Parse((string)this.Connections.Rows[i].Cells[0].Value) == indenty)
                    {
                        this.Connections.Rows[i].DefaultCellStyle.BackColor = color;
                        break;
                    }
                }
            }
        }
        public void ConnectionsRowDelete(Guid indenty)
        {
            if (this.Connections.InvokeRequired)
            {
                RowDeleteCallback d = new RowDeleteCallback(ConnectionsRowDelete);
                this.BeginInvoke(d, new object[] { indenty });
            }
            else
            {
                for (int i = 0; i < this.Connections.Rows.Count; i++)
                {
                    if (Guid.Parse((string)this.Connections.Rows[i].Cells[0].Value) == indenty)
                    {
                        this.Connections.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        public void ConnectionsRowsClear()
        {
            if (this.Connections.InvokeRequired)
            {
                Callback d = new Callback(ConnectionsRowsClear);
                this.BeginInvoke(d, new object[] { });
            }
            else
                this.Connections.Rows.Clear();
        }
        public void ConnectionsEditRow(Guid indenty, int cellIndex, string s)
        {
            if (this.Connections.InvokeRequired)
            {
                RowsEditCallback d = new RowsEditCallback(ConnectionsEditRow);
                this.BeginInvoke(d, new object[] { indenty, cellIndex, s });
            }
            else
            {
                for (int i = 0; i < this.Connections.Rows.Count; i++)
                {
                    if (Guid.Parse((string)this.Connections.Rows[i].Cells[0].Value) == indenty)
                    {
                        this.Connections.Rows[i].Cells[cellIndex].Value = s;
                        break;
                    }
                }
            }

        }
        public int ConnectionsAddRow(params object[] values)
        {
            if (this.Connections.InvokeRequired)
            {
                RowsAddCallback d = new RowsAddCallback(ConnectionsAddRow);
                this.BeginInvoke(d, new object[] { values });
                return 0;
            }
            else
                return this.Connections.Rows.Add(values);
        }

        private void lbl_TotalSetText(string text)
        {
            if (this.lbl_Total.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(lbl_TotalSetText);
                this.BeginInvoke(d, new object[] { text });
            }
            else
                this.lbl_Total.Text = text;
        }
        #endregion

        #region MoveWindows
        private int mov, movY, movX;

        private void DragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void DragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void DragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Title_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }
        #endregion

        private List<Thread> Connects = new List<Thread>();
        private void ProxyConnect(FormMain _form, string _server, int _steamid, int _interval, int _timeout)
        {
            List<List<string>> SplitThread = new List<List<string>>();
            var Proxys = ProxyList.Lines.ToList();
            int ThreadCount = int.Parse(tb_Threads.Text);
            int nSize = (Proxys.Count / ThreadCount);

            for (int i = 0; i < Proxys.Count; i += nSize)
                SplitThread.Add(Proxys.GetRange(i, Math.Min(nSize, Proxys.Count - i)));

            foreach (var l in SplitThread)
            {
                Connects.Add(new Thread(() =>
                {
                    while (isRunning)
                    {
                        Thread.Sleep(10);
                        foreach (var item in l)
                        {
                            if (!isRunning)
                                break;
                            Thread.Sleep(10);
                            if (ActiveConnects.Count >= NeededUser || (ActiveProxyTunnel + ConnectedUser) > (NeededUser + 2))
                                continue;
                            try
                            {
                                ActiveProxyTunnel++;
                                string[] array;
                                array = item.Split(new char[] { ':' });
                                if (Banned.Any(x => x == array[0]) || clients.Any(client => client.Proxyaddress == array[0]))
                                {
                                    if (ActiveProxyTunnel > 0) ActiveProxyTunnel--;
                                    continue;
                                }
                                
                                var user = new User(_form, GetRandomName(), _server, _steamid, _interval, array[0], Convert.ToInt32(array[1]), _timeout);
                                if (user.Connect() && ActiveConnects.Count <= NeededUser)
                                    clients.Add(user);
                                else
                                {
                                    //try { if (user.Proxy != null) user.Proxy.Close(); } catch (Exception) { }
                                    ConnectionsRowDelete(user.Identity);
                                }
                                if(ActiveProxyTunnel > 0) ActiveProxyTunnel--;
                            }
                            catch (Exception)
                            {
                                if (ActiveProxyTunnel > 0) ActiveProxyTunnel--;
                            }
                        }
                    }
                    PrintC($"Thread exited (0x{Thread.CurrentThread.ManagedThreadId.ToString("X")})", Color.Red);
                }));
            }

            foreach (Thread item in Connects)
                item.Start();
        }

        private void SwapReadOnly(params Control[] Controls)
        {
            foreach (TextBox control in Controls.ToList())
                control.ReadOnly = !control.ReadOnly;
        }
        private void SwapEnable(params Control[] Controls)
        {
            foreach (Control control in Controls.ToList())
                control.Enabled = !control.Enabled;
        }

        private int NeededUser = 0;
        private int ConnectedUser = 0;
        private void btn_Start_Click(object sender, EventArgs e)
        {
            PrintC($"[STARTED] IP: {ServerList.Text} | Proxy: {cb_Proxy.Checked}({ProxyList.Lines.Length}) | Name: {cb_NameText.Checked}", Color.Cyan);
            isRunning = true;
            taskrun = true;
            SwapReadOnly(
                ServerList, 
                ProxyList, 
                ProxyList, 
                NameText, 
                MaxConnect,
                RefreshRate,
                tb_Threads,
                tb_Timeout,
                tb_password
            );

            SwapEnable(
                cb_Proxy,
                cb_NameText,
                btn_Start,
                btn_Stop,
                steamid
            );

            if (cb_Proxy.Checked)
                ProxyConnect(this, ServerList.Lines[0], steamid.SelectedIndex, int.Parse(RefreshRate.Text), int.Parse(tb_Timeout.Text));

            Refresher.Start();

            NeededUser = int.Parse(MaxConnect.Text) + 4;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            ShutdownConnect();
        }

        private void ShutdownConnect()
        {
            PrintC($"[STOPED] IP: {ServerList.Text} | Proxy: {cb_Proxy.Checked}({ProxyList.Lines.Length}) | Name: {cb_NameText.Checked}", Color.Cyan);
            Refresher.Stop();
            isRunning = false;
            taskrun = false;
            SteamReject = 0;
            InvalidPass = 0;
            ActiveProxyTunnel = 0;
            ConnectedUser = 0;
            lbl_Output.Text = "";

            foreach (var client in clients)
            {
                try { if (client.Proxy != null) client.Proxy.Close(); } catch (Exception) { }
                ConnectionsRowDelete(client.Identity);
            }
            Connects.Clear();
            Connections.Rows.Clear();
            ActiveConnects.Clear();
            clients.Clear();
            Banned.Clear();

            SwapReadOnly(
                ServerList,
                ProxyList,
                ProxyList,
                NameText,
                MaxConnect,
                RefreshRate,
                tb_Threads,
                tb_Timeout,
                tb_password
            );

            SwapEnable(
                cb_Proxy,
                cb_NameText,
                btn_Start,
                btn_Stop,
                steamid
            );
        }

        bool taskrun = true;
        Dictionary<Guid, Task> ActiveConnects = new Dictionary<Guid, Task>();

        private void Refresher_Tick(object sender, EventArgs e)
        {
            if (SteamReject > 10)
            {
                ShutdownConnect();
                MessageBox.Show("Too many STEAM validation rejected");
            }
            if (InvalidPass > 10)
            {
                ShutdownConnect();
                MessageBox.Show("Wrong/No password set");
            }

            var tempClients = clients.ToList();
            lbl_Output.Text =
                $" | ThreadActive: {Connects.Where(x => x.IsAlive).Count()}" +
                $" | AliveConnect: {tempClients.Where(x => x.Status == state.Connected).Count()}/{tempClients.Count}" + 
                //" | R:" + ReUsed +
                $" | Con: {ConnectedUser}/{NeededUser}" +
                $" | APT: {ActiveProxyTunnel}" +
                $" | BND: {Banned.Count}";
            foreach (var item in tempClients)
            {
                item.UpdateInfo();
                if ((item.Status == state.Connecting || item.Status == state.Connected) && !ActiveConnects.ContainsKey(item.Identity) && ConnectedUser < NeededUser)
                {
                    Task temp = new Task(() =>
                    {
                        ConnectedUser++;
                        long NextUpdate = DateTime.Now.Ticks + item.Interval * 10000;//10,000,000 <= Sleep(1000)
                        while (taskrun)
                        {
                            Thread.Sleep(10);
                            if (NextUpdate <= DateTime.Now.Ticks)
                            {
                                item.timer_Tick();
                                if (item.Kill)
                                {
                                    ConnectedUser--;
                                    PrintC($"[{item.Identity}] | TERMINATED |", Color.Red);
                                    try { if (item.Proxy != null) item.Proxy.Close(); } catch (Exception) { }
                                    ConnectionsRowDelete(item.Identity);
                                    if (ActiveConnects.ContainsKey(item.Identity))
                                        ActiveConnects.Remove(item.Identity);

                                    clients.Remove(item);
                                    break;
                                }
                                

                                NextUpdate = DateTime.Now.Ticks + item.Interval * 10000;
                            }
                        }
                    });
                    temp.Start();
                    ActiveConnects.Add(item.Identity, temp);
                }
            }
        }

        public string GetRandomName()
        {
            string _nick = string.Empty;
            do
            {
                if (cb_NameText.Checked)
                    _nick = NameText.Text + "_" + RNDGEN.RandomString(6);
                else
                    _nick = names[RNDGEN.rnd.Next(0, names.Count())]
                        .Replace(" ", "_")
                        .Replace("{", "[")
                        .Replace("}", "]")
                        .Replace("(", "[")
                        .Replace(")", "]");
            } while (clients.Where(c => c.Nick == _nick).Count() > 0);

            return _nick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProxyList_TextChanged(object sender, EventArgs e)
        {
            lbl_TotalSetText("Total: " + ProxyList.Lines.Length);
        }

        private void cb_NameText_CheckedChanged(object sender, EventArgs e)
        {
            if (names.Count() < 40)
            {
                MessageBox.Show("Name from file", $"You only added {names.Count()} name into the names.txt,\nyou should write minimum 40!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            Refresher.Enabled = false;
            isRunning = false;
            taskrun = false;
            lbl_Output.Text = "";

            foreach (Thread item in Connects)
                item.Abort();

            foreach (var client in clients)
                try { if (client.Proxy != null) client.Proxy.Close(); } catch (Exception) { }

            int act = Connects.Where(x => x.IsAlive).Count();
            if (act > 0)
            {
                p_msgbox.Visible = true;
                p_msgbox.Update();
            }
            while (act > 0)
            {
                lbl_msgbox.Text = $"Alive thread left: {act}\r\n(The program close after this)";
                lbl_msgbox.Update();
                Thread.Sleep(10);
                act = Connects.Where(x => x.IsAlive).Count();
            }
            p_msgbox.Visible = true;
            p_msgbox.Update();
            lbl_msgbox.Text = $"Bye :)";
            lbl_msgbox.Update();
            Thread.Sleep(1000);
        }

        private void rtb_Console_TextChanged(object sender, EventArgs e)
        {
            if (cb_AutoScroll.Checked)
            { 
                rtb_Console.SelectionStart = rtb_Console.Text.Length;
                rtb_Console.ScrollToCaret();
            }
        }

        private void lbl_msgbox_Click(object sender, EventArgs e)
        {

        }

        private void Connections_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            int currentMouseOverRow = Connections.HitTest(e.X, e.Y).RowIndex;

            var m = new ContextMenuStrip();

            if (currentMouseOverRow >= 0)
            {
                Connections.Rows[currentMouseOverRow].Selected = true;
                m.Items.Add(new ToolStripMenuItem() { Text = "rename", Name = "Random name", Tag = currentMouseOverRow });
                m.Items.Add(new ToolStripMenuItem() { Text = "remove", Name = "Remove", Tag = currentMouseOverRow });
                m.Show(Connections, new Point(e.X, e.Y));
                m.ItemClicked += M_ItemClicked;
            }
        }

        private void M_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var cell_identy = Guid.Parse((string)this.Connections.Rows[(int)e.ClickedItem.Tag].Cells[0].Value);
            if (clients.Any(user => user.Identity == Guid.Parse((string)this.Connections.Rows[(int)e.ClickedItem.Tag].Cells[0].Value)))
            {
                User user = clients.First(x => x.Identity == Guid.Parse((string)this.Connections.Rows[(int)e.ClickedItem.Tag].Cells[0].Value));
                switch (e.ClickedItem.Text)
                {
                    case "rename":
                        var newname = GetRandomName();
                        user.Rename(newname);
                        ConnectionsEditRow(user.Identity, 1, newname);
                        break;
                    case "remove":
                        try { user.Proxy.Close(); } catch (Exception) {}
                        ConnectionsRowDelete(user.Identity);
                        if (ActiveConnects.ContainsKey(user.Identity))
                        {
                            user.Kill = true;
                            PrintC($"[{user.Identity}] | TERMINATED | REASON: By user input", Color.Red);
                            ActiveConnects.Remove(user.Identity);
                        }
                        clients.Remove(user);
                        break;
                    default:
                        break;
                }
            }
        }

       
    }
}
