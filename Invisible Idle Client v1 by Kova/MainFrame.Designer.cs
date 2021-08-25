namespace IICv2
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ServerList = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NameText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.steamid = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Connections = new System.Windows.Forms.DataGridView();
            this.Identy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proxy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RefreshRate = new System.Windows.Forms.TextBox();
            this.MaxConnect = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.Refresher = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.DragPanel = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbl_Total = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ProxyList = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_Timeout = new System.Windows.Forms.TextBox();
            this.tb_Threads = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.p_msgbox = new System.Windows.Forms.Panel();
            this.lbl_msgbox = new System.Windows.Forms.Label();
            this.cb_Proxy = new System.Windows.Forms.CheckBox();
            this.cb_NameText = new System.Windows.Forms.CheckBox();
            this.cb_AutoScroll = new System.Windows.Forms.CheckBox();
            this.lbl_Output = new System.Windows.Forms.Label();
            this.rtb_Console = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Connections)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.DragPanel.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.p_msgbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerList
            // 
            this.ServerList.BackColor = System.Drawing.Color.Black;
            this.ServerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerList.ForeColor = System.Drawing.Color.Aqua;
            this.ServerList.Location = new System.Drawing.Point(6, 19);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(273, 26);
            this.ServerList.TabIndex = 0;
            this.ServerList.Text = "172.0.0.1:27015";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_password);
            this.groupBox1.Controls.Add(this.ServerList);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 188);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(67, 51);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(212, 26);
            this.tb_password.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "Pass:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Servers";
            // 
            // NameText
            // 
            this.NameText.Location = new System.Drawing.Point(6, 15);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(272, 20);
            this.NameText.TabIndex = 2;
            this.NameText.TextChanged += new System.EventHandler(this.NameText_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NameText);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox2.Location = new System.Drawing.Point(294, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 46);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Prefix";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.steamid);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox3.Location = new System.Drawing.Point(294, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 54);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // steamid
            // 
            this.steamid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.steamid.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.steamid.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid.IntegralHeight = false;
            this.steamid.Items.AddRange(new object[] {
            "RevEmu",
            "OldRevEmu",
            "SteamEmu",
            "AVSMP 1",
            "AVSMP 0",
            "SettiEmu",
            "NativeSteam"});
            this.steamid.Location = new System.Drawing.Point(6, 20);
            this.steamid.Name = "steamid";
            this.steamid.Size = new System.Drawing.Size(145, 21);
            this.steamid.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Emulator";
            // 
            // Connections
            // 
            this.Connections.AllowUserToAddRows = false;
            this.Connections.AllowUserToDeleteRows = false;
            this.Connections.AllowUserToResizeRows = false;
            this.Connections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Connections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Identy,
            this.Nick,
            this.Proxy,
            this.Status,
            this.PF,
            this.NR});
            this.Connections.Location = new System.Drawing.Point(3, 198);
            this.Connections.MultiSelect = false;
            this.Connections.Name = "Connections";
            this.Connections.ReadOnly = true;
            this.Connections.RowHeadersVisible = false;
            this.Connections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Connections.Size = new System.Drawing.Size(575, 271);
            this.Connections.TabIndex = 5;
            // 
            // Identy
            // 
            this.Identy.HeaderText = "Identy";
            this.Identy.Name = "Identy";
            this.Identy.ReadOnly = true;
            this.Identy.Visible = false;
            // 
            // Nick
            // 
            this.Nick.HeaderText = "Nick";
            this.Nick.Name = "Nick";
            this.Nick.ReadOnly = true;
            this.Nick.Width = 130;
            // 
            // Proxy
            // 
            this.Proxy.HeaderText = "Proxy";
            this.Proxy.Name = "Proxy";
            this.Proxy.ReadOnly = true;
            this.Proxy.Width = 150;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PF
            // 
            this.PF.HeaderText = "PF";
            this.PF.MinimumWidth = 25;
            this.PF.Name = "PF";
            this.PF.ReadOnly = true;
            this.PF.Width = 25;
            // 
            // NR
            // 
            this.NR.HeaderText = "NR";
            this.NR.MinimumWidth = 25;
            this.NR.Name = "NR";
            this.NR.ReadOnly = true;
            this.NR.Width = 25;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.RefreshRate);
            this.groupBox4.Controls.Add(this.MaxConnect);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Bold);
            this.groupBox4.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox4.Location = new System.Drawing.Point(294, 119);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 73);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Refresh Rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Max Connect:";
            // 
            // RefreshRate
            // 
            this.RefreshRate.Location = new System.Drawing.Point(104, 41);
            this.RefreshRate.Name = "RefreshRate";
            this.RefreshRate.Size = new System.Drawing.Size(67, 20);
            this.RefreshRate.TabIndex = 6;
            this.RefreshRate.Text = "5000";
            this.RefreshRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RefreshRate.TextChanged += new System.EventHandler(this.RefreshRate_TextChanged);
            this.RefreshRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // MaxConnect
            // 
            this.MaxConnect.Location = new System.Drawing.Point(104, 14);
            this.MaxConnect.Name = "MaxConnect";
            this.MaxConnect.Size = new System.Drawing.Size(24, 20);
            this.MaxConnect.TabIndex = 6;
            this.MaxConnect.Text = "32";
            this.MaxConnect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxConnect.TextChanged += new System.EventHandler(this.MaxConnect_TextChanged);
            this.MaxConnect.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // btn_Start
            // 
            this.btn_Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.ForeColor = System.Drawing.Color.Aqua;
            this.btn_Start.Location = new System.Drawing.Point(3, 475);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(135, 23);
            this.btn_Start.TabIndex = 16;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = false;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Stop.Enabled = false;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.ForeColor = System.Drawing.Color.Aqua;
            this.btn_Stop.Location = new System.Drawing.Point(443, 475);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(135, 23);
            this.btn_Stop.TabIndex = 16;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // Refresher
            // 
            this.Refresher.Interval = 1;
            this.Refresher.Tick += new System.EventHandler(this.Refresher_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(906, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DragPanel
            // 
            this.DragPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DragPanel.Controls.Add(this.Title);
            this.DragPanel.Location = new System.Drawing.Point(0, 0);
            this.DragPanel.Name = "DragPanel";
            this.DragPanel.Size = new System.Drawing.Size(906, 23);
            this.DragPanel.TabIndex = 18;
            this.DragPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragPanel_MouseDown);
            this.DragPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragPanel_MouseMove);
            this.DragPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DragPanel_MouseUp);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Title.ForeColor = System.Drawing.Color.Aqua;
            this.Title.Location = new System.Drawing.Point(3, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(0, 20);
            this.Title.TabIndex = 7;
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            this.Title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Title_MouseUp);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbl_Total);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.ProxyList);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.tb_Timeout);
            this.groupBox5.Controls.Add(this.tb_Threads);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox5.Location = new System.Drawing.Point(584, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(323, 465);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            // 
            // lbl_Total
            // 
            this.lbl_Total.AutoSize = true;
            this.lbl_Total.Location = new System.Drawing.Point(6, 442);
            this.lbl_Total.Name = "lbl_Total";
            this.lbl_Total.Size = new System.Drawing.Size(69, 20);
            this.lbl_Total.TabIndex = 9;
            this.lbl_Total.Text = "Total: 0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(151, 412);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 20);
            this.label10.TabIndex = 7;
            this.label10.Text = "Timeout:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 412);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Threads:";
            // 
            // ProxyList
            // 
            this.ProxyList.BackColor = System.Drawing.Color.Black;
            this.ProxyList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProxyList.ForeColor = System.Drawing.Color.Aqua;
            this.ProxyList.Location = new System.Drawing.Point(6, 19);
            this.ProxyList.MaxLength = 2147483647;
            this.ProxyList.Multiline = true;
            this.ProxyList.Name = "ProxyList";
            this.ProxyList.Size = new System.Drawing.Size(311, 383);
            this.ProxyList.TabIndex = 0;
            this.ProxyList.TextChanged += new System.EventHandler(this.ProxyList_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Proxys";
            // 
            // tb_Timeout
            // 
            this.tb_Timeout.Location = new System.Drawing.Point(240, 409);
            this.tb_Timeout.Name = "tb_Timeout";
            this.tb_Timeout.Size = new System.Drawing.Size(73, 26);
            this.tb_Timeout.TabIndex = 6;
            this.tb_Timeout.Text = "3000";
            this.tb_Timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Timeout.TextChanged += new System.EventHandler(this.tb_Timeout_TextChanged);
            this.tb_Timeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // tb_Threads
            // 
            this.tb_Threads.Location = new System.Drawing.Point(99, 409);
            this.tb_Threads.Name = "tb_Threads";
            this.tb_Threads.Size = new System.Drawing.Size(48, 26);
            this.tb_Threads.TabIndex = 6;
            this.tb_Threads.Text = "30";
            this.tb_Threads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Threads.TextChanged += new System.EventHandler(this.tb_Threads_TextChanged);
            this.tb_Threads.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.p_msgbox);
            this.splitContainer1.Panel1.Controls.Add(this.cb_Proxy);
            this.splitContainer1.Panel1.Controls.Add(this.cb_NameText);
            this.splitContainer1.Panel1.Controls.Add(this.Connections);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Stop);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Start);
            this.splitContainer1.Panel1MinSize = 335;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cb_AutoScroll);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_Output);
            this.splitContainer1.Panel2.Controls.Add(this.rtb_Console);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(929, 514);
            this.splitContainer1.SplitterDistance = 485;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 20;
            // 
            // p_msgbox
            // 
            this.p_msgbox.Controls.Add(this.lbl_msgbox);
            this.p_msgbox.Location = new System.Drawing.Point(340, 180);
            this.p_msgbox.Name = "p_msgbox";
            this.p_msgbox.Size = new System.Drawing.Size(200, 100);
            this.p_msgbox.TabIndex = 20;
            this.p_msgbox.Visible = false;
            // 
            // lbl_msgbox
            // 
            this.lbl_msgbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_msgbox.ForeColor = System.Drawing.Color.Aqua;
            this.lbl_msgbox.Location = new System.Drawing.Point(0, 0);
            this.lbl_msgbox.Name = "lbl_msgbox";
            this.lbl_msgbox.Size = new System.Drawing.Size(200, 100);
            this.lbl_msgbox.TabIndex = 0;
            this.lbl_msgbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_msgbox.Click += new System.EventHandler(this.lbl_msgbox_Click);
            // 
            // cb_Proxy
            // 
            this.cb_Proxy.AutoSize = true;
            this.cb_Proxy.Location = new System.Drawing.Point(580, 7);
            this.cb_Proxy.Name = "cb_Proxy";
            this.cb_Proxy.Size = new System.Drawing.Size(15, 14);
            this.cb_Proxy.TabIndex = 9;
            this.cb_Proxy.UseVisualStyleBackColor = true;
            // 
            // cb_NameText
            // 
            this.cb_NameText.AutoSize = true;
            this.cb_NameText.Checked = true;
            this.cb_NameText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_NameText.Location = new System.Drawing.Point(288, 7);
            this.cb_NameText.Name = "cb_NameText";
            this.cb_NameText.Size = new System.Drawing.Size(15, 14);
            this.cb_NameText.TabIndex = 9;
            this.cb_NameText.UseVisualStyleBackColor = true;
            this.cb_NameText.CheckedChanged += new System.EventHandler(this.cb_NameText_CheckedChanged);
            // 
            // cb_AutoScroll
            // 
            this.cb_AutoScroll.AutoSize = true;
            this.cb_AutoScroll.Checked = true;
            this.cb_AutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_AutoScroll.ForeColor = System.Drawing.Color.Aqua;
            this.cb_AutoScroll.Location = new System.Drawing.Point(843, 5);
            this.cb_AutoScroll.Name = "cb_AutoScroll";
            this.cb_AutoScroll.Size = new System.Drawing.Size(74, 17);
            this.cb_AutoScroll.TabIndex = 21;
            this.cb_AutoScroll.Text = "AutoScroll";
            this.cb_AutoScroll.UseVisualStyleBackColor = true;
            // 
            // lbl_Output
            // 
            this.lbl_Output.AutoSize = true;
            this.lbl_Output.ForeColor = System.Drawing.Color.Aqua;
            this.lbl_Output.Location = new System.Drawing.Point(89, 6);
            this.lbl_Output.Name = "lbl_Output";
            this.lbl_Output.Size = new System.Drawing.Size(25, 13);
            this.lbl_Output.TabIndex = 20;
            this.lbl_Output.Text = "------";
            // 
            // rtb_Console
            // 
            this.rtb_Console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtb_Console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Console.Location = new System.Drawing.Point(11, 23);
            this.rtb_Console.Name = "rtb_Console";
            this.rtb_Console.ReadOnly = true;
            this.rtb_Console.Size = new System.Drawing.Size(906, 134);
            this.rtb_Console.TabIndex = 8;
            this.rtb_Console.Text = "";
            this.rtb_Console.TextChanged += new System.EventHandler(this.rtb_Console_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Aqua;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "Console";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(930, 545);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.DragPanel);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(599, 528);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invisible Idle Player v2.0 by Kova";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Connections)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.DragPanel.ResumeLayout(false);
            this.DragPanel.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.p_msgbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ServerList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox NameText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox steamid;
        private System.Windows.Forms.DataGridView Connections;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MaxConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RefreshRate;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Timer Refresher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel DragPanel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox ProxyList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cb_Proxy;
        private System.Windows.Forms.CheckBox cb_NameText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_Threads;
        private System.Windows.Forms.Label lbl_Total;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_Timeout;
        private System.Windows.Forms.Label lbl_Output;
        private System.Windows.Forms.DataGridViewTextBoxColumn Identy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nick;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proxy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn PF;
        private System.Windows.Forms.DataGridViewTextBoxColumn NR;
        public System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.RichTextBox rtb_Console;
        private System.Windows.Forms.CheckBox cb_AutoScroll;
        private System.Windows.Forms.Panel p_msgbox;
        private System.Windows.Forms.Label lbl_msgbox;
    }
}

