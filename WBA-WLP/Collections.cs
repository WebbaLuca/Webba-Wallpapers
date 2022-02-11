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
using Webba_Wallpapers;

namespace Webba_Wallpapers
{
    public partial class Collections : Form
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

        public string activeCollection;
        public int changeWhere;

        public Collections()
        {
            InitializeComponent();

            this.Hide();

            checkCollection();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 35, 35));

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/wallpapers.html");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/wallpapers.html");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/wallpapers.html");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://webba-creative.com/en/index.html");
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text != "Active")
            {
                button4.Text = "Active";
                button5.Text = "Installed";

                

                String path = Application.StartupPath + "/activecollection.txt";
                try
                {
                    StreamWriter sr = new StreamWriter(path);
                
                    sr.WriteLine("Webba");
                    sr.Close();

                    Console.WriteLine(File.ReadAllText(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                checkCollection();

                string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + activeCollection + "/01 .jpg";

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

        

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text != "Active")
            {
                button5.Text = "Active";
                button4.Text = "Installed";

                String path = Application.StartupPath + "/activecollection.txt";
                try
                {
                    StreamWriter sr = new StreamWriter(path);

                    sr.WriteLine("PJ Croce");
                    sr.Close();

                    Console.WriteLine(File.ReadAllText(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                checkCollection();

                string picsOfTheDay = "https://software.webba-creative.com/wallpapers/" + activeCollection + "/01 .jpg";

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

        void checkCollection()
        {
            //Recherche de la collection active
            String ColLine;
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(Application.StartupPath + "/activecollection.txt");
                StreamReader sr = new StreamReader(stream);
                ColLine = sr.ReadLine();
                while (ColLine != null)
                {
                    Console.WriteLine("Actual Collection " + ColLine);

                    if (ColLine == "Webba")
                    {
                        button4.Text = "Active";
                        button5.Text = "Installed";
                    }
                    else if (ColLine == "PJ Croce")
                    {
                        button5.Text = "Active";
                        button4.Text = "Installed";
                    }

                    activeCollection = ColLine;

                    ColLine = sr.ReadLine();

                }
                sr.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(button7.Text == "Add")
            {
                panel10.Visible = true;
                changeWhere = 4;
            }
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;
            textBox1.Text = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;
            textBox1.Text = null;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (button3.Text == "Add")
            {
                panel10.Visible = true;
                changeWhere = 5;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "Add")
            {
                panel10.Visible = true;
                changeWhere = 6;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == "Add")
            {
                panel10.Visible = true;
                changeWhere = 7;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "Add")
            {
                panel10.Visible = true;
                changeWhere = 3;
            }
        }
    }
}
