using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace KeyAuth
{
    public partial class Login : Form
    {
        public static api KeyAuthApp = new api(
            name: "Oblivity Software LLC",
            ownerid: "RQD7zNX0Vc",
            secret: "6e39867b070fe7f41aaede89ddb538f1d2b28c8e9be910c6306a5037603f0db9",
            version: "1.0"
        );

 
        public Login()
        {
            InitializeComponent();

            
            this.Text = random_string();
        }

 
        static string random_string()
        {
            string str = null;
            Random random = new Random();

          
            for (int i = 0; i < 9; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;
        }

   

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public static bool SubExist(string name)
        {
            if (KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name))
                return true;
            return false;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();

            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    DialogResult dialogResult = MessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case DialogResult.No:
                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            MessageBox.Show("Invalid option");
                            Environment.Exit(0);
                            break;
                    }
                }
                MessageBox.Show("Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer");
                Environment.Exit(0);
            }

            if (!KeyAuthApp.response.success)
            {
                MessageBox.Show(KeyAuthApp.response.message);
                Environment.Exit(0);
            }
        }

        private void UpgradeBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.upgrade(username.Text, key.Text);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.login(username.Text, password.Text);
            if (KeyAuthApp.response.success)
            {


             
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dontrunme.exe");
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
                Application.Exit(); 
            }
            else
            {
                MessageBox.Show("Incorrect Login Or Invalid HWID, Contact support for a HWID reset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RgstrBtn_Click(object sender, EventArgs e)
        {
            string email = this.email.Text;
            if (email == "Email (leave blank if none)")
            {
                email = null;
            }

            KeyAuthApp.register(username.Text, password.Text, key.Text, email);
            if (KeyAuthApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("incorrect login");
            }
        }


        private void LicBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.license(key.Text);
            if (KeyAuthApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect Login Or Invaild HWID, Contact support for a hwid reset");
            }

        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            KeyAuthApp.forgot(username.Text, email.Text);

        }
    }
}
