using System;
using System.Text;
using System.Windows.Forms;

namespace IICv2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
        #if DEBUG
                        args = new string[1];
                        args[0] = "IdŁ3PŁ4Y3Rv1BYK0V4";
        #endif

            if (args.Length == 1)
            {
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(args[0]);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }

                    if (sb.ToString() == "28E69606EC4BC57D9408AE4DBC4B2EE9")
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new FormMain());
                    }
                }
            }
        }
    }
}
