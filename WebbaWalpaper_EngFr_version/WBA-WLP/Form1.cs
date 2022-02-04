using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace WBA_WLP
{
    public partial class Form1 : Form
    {

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
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.SetValue("WBA-WLP", Application.ExecutablePath);

        }


        public Form1()
        {
            InitializeComponent();

            SetStartup();


            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("WBA-WLP", Application.ExecutablePath);

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 35, 35));

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

        // Bouton pour passer le from en français
        public void label5_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            button3.Hide();
            button1.Hide();
            button4.Hide();
            label5.Hide();
            label6.Show();
            pictureBox3.Show();
            button_suivant.Show();
            button_apropos.Show();
            button_quitter.Show();
            label2.Show();
            label3.Show();
            label4.Show();
        }
        // Bouton pour passer le form en anglais
        private void label6_Click(object sender, EventArgs e)
        {
            pictureBox2.Show();
            label5.Show();
            button1.Show();
            button3.Show();
            button4.Show();
            pictureBox3.Hide();
            button_suivant.Hide();
            label2.Hide();
            button_apropos.Hide();
            label3.Hide();
            button_quitter.Hide();
            label4.Hide();
            label6.Hide();
        }

        /// <Explication>
        /// ligne 107 -> bouton pour image suivante
        /// ligne 175 -> l'icon de notification
        /// ligne 184 -> bouton reload
        /// 
        /// </Fin Explication>

        // Bouton image suivante
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


        // Bouton "icon notifictaion"
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }


        // Bouton "reload"
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


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/index.html");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/wallpapers.html");
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }




        // Partie FR

        // Bouton reload
        private void button_suivant_Click(object sender, EventArgs e)
        {
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
        }

        private void label2_Click(object sender, EventArgs e)
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
        // Fin bouton reload


        // Bouton wallpapers
        private void button_apropos_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/fr/wallpapers.html");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/fr/wallpapers.html");
        }

        
        // Fin bouton wallpapers

        // Bouton a propos
        private void button_quitter_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon.Visible = true;
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/fr/index.html");
        }
        // Fin bouton a propos
    }
}
