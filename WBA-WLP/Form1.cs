using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace WBA_WLP
{
    

    public partial class Form1 : Form
    {
        //RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, 
            int nTopRect,    
            int nRightRect, 
            int nBottomRect,   
            int nWidthEllipse, 
            int nHeightEllipse 
        );

        private void SetStartup()
        {

            //registryKey.SetValue("WBA-WLP", Application.ExecutablePath);

        }

        public void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }

        public Form1()
        {

            InitializeComponent();

            SetStartup();

            //Version verification
            String line;
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://software.webba-creative.com/wallpapers/version.txt");
                StreamReader sr = new StreamReader(stream);
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine("Actual Version " + line);

                    //If download version == last version
                    if (line.Contains("1.1.2"))
                    {
                        //Don't open Update manager
                        Console.WriteLine("App up to date");
                    }
                    else
                    {
                        //Open Update manager
                        //System.Diagnostics.Process.Start(Application.StartupPath + "/Webba updater.exe");
                        ExecuteAsAdmin(Application.StartupPath + "/Webba updater.exe");
                        Console.WriteLine("Update available");
                    }

                    line = sr.ReadLine();

                }
                sr.Close();               

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }


            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 35, 35));

            string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + DateTime.Now.ToString("d tt") + ".jpg";

            Console.WriteLine(DateTime.Now.ToString("d tt"));

            string root = @"C:\Webba Wallpapers\";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            FileIOPermission file = new FileIOPermission(PermissionState.None);
            file.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                file.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }




            string localFilename = @"C:/Webba Wallpapers/picsOfTheDay.png";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(picsOfTheDay, localFilename);
            }

            string photo = "C:/Webba Wallpapers/picsOfTheDay.png";
            SetWallpaper(photo);

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string valPic = "19";

            Random r = new Random();
            int rInt = r.Next(1, 31); //for ints

            if (rInt <= 9)
            {
               valPic = "0" + rInt.ToString();
            }
            else
            {
               valPic = rInt.ToString();
            }

            Console.WriteLine(rInt.ToString());

            string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + valPic + " .jpg";

            string root = @"C:\Webba Wallpapers\";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            FileIOPermission file = new FileIOPermission(PermissionState.None);
            file.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                file.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }




            string localFilename = @"C:/Webba Wallpapers/picsOfTheDay.png";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(picsOfTheDay, localFilename);
            }

            string photo = "C:/Webba Wallpapers/picsOfTheDay.png";
            SetWallpaper(photo);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        public static void SetWallpaper(String path)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/wallpapers.html");
        }


        private void button1_Click(object sender, EventArgs e)
        {
                Hide();
                notifyIcon.Visible = true;
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Random r = new Random();
            int rInt = r.Next(0, 31); //for ints

            string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + rInt + " .jpg";

            string root = @"C:\Webba Wallpapers\";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            FileIOPermission file = new FileIOPermission(PermissionState.None);
            file.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                file.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }




            string localFilename = @"C:/Webba Wallpapers/picsOfTheDay.png";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(picsOfTheDay, localFilename);
            }

            string photo = "C:/Webba Wallpapers/picsOfTheDay.png";
            SetWallpaper(photo);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void aboutWebbaWallpapersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/wallpapers.html");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.ToString("t") == "00:00")
            {

                string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + DateTime.Now.ToString("d tt") + ".jpg";

                string root = @"C:\Webba Wallpapers\";

                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                FileIOPermission file = new FileIOPermission(PermissionState.None);
                file.AllLocalFiles = FileIOPermissionAccess.Read;
                try
                {
                    file.Demand();
                }
                catch (SecurityException s)
                {
                    Console.WriteLine(s.Message);
                }




                string localFilename = @"C:/Webba Wallpapers/picsOfTheDay.png";
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(picsOfTheDay, localFilename);
                }

                string photo = "C:/Webba Wallpapers/picsOfTheDay.png";
                SetWallpaper(photo);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + DateTime.Now.ToString("d tt") + ".jpg";

            string root = @"C:\Webba Wallpapers\";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            FileIOPermission file = new FileIOPermission(PermissionState.None);
            file.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                file.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }

            string localFilename = @"C:/Webba Wallpapers/picsOfTheDay.png";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(picsOfTheDay, localFilename);
            }

            string photo = "C:/Webba Wallpapers/picsOfTheDay.png";
            SetWallpaper(photo);
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/index.html");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //Version verification
            String line;
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://software.webba-creative.com/wallpapers/version.txt");
                StreamReader sr = new StreamReader(stream);
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine("Actual Version " + line);

                    //If download version == last version
                    if (line.Contains("1.1.2"))
                    {
                        //Don't open Update manager
                        Console.WriteLine("App up to date");
                    }
                    else
                    {
                        //Open Update manager
                        System.Diagnostics.Process.Start(Application.StartupPath + "/Webba updater.exe");
                        Console.WriteLine("Update available");
                    }

                    line = sr.ReadLine();

                }
                sr.Close();


            }
            catch
            {
                Console.WriteLine("Updated");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
           
        }

        Point lastPoint;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
      
        }
    }
}
