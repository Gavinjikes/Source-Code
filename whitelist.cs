using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using KeyAuth;

namespace Loader
{
    public partial class whitelist : Form
    {
        public whitelist()
        {
            InitializeComponent();
            this.Text = GenerateRandomString(9);
            CheckWebsiteAndProceed();
        }

        private async void CheckWebsiteAndProceed()
        {
            progressBar1.Style = ProgressBarStyle.Continuous; 
            progressBar1.Minimum = 0; 
            progressBar1.Maximum = 100; 

            for (int i = 0; i <= 100; i++) 
            {
                progressBar1.Value = i; 
                await Task.Delay(1);
            }

            bool isOnline = await PingWebsite("oblivity.xyz");

            if (!isOnline)
            {
                MessageBox.Show("Failed to contact oblivity.xyz. Please try using a VPN or verify the oblivity website is online.", "Error");
                this.Close();
            }
            else
            {
                await Task.Delay(1000); 
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
        }

        private async Task<bool> PingWebsite(string url)
        {
            bool isOnline = false;
            try
            {
                Ping ping = new Ping();
                PingReply reply = await ping.SendPingAsync(url);
                isOnline = (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                isOnline = false;
            }
            return isOnline;
        }

        private string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void whitelist_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
