using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace KeyAuth
{
    public partial class Main : Form
    {
        private CheckBoxState checkBoxState;

        public Main()
        {
            InitializeComponent();
            checkBoxState = new CheckBoxState();
            LoadCheckBoxState(); 
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

        private void LoadCheckBoxState()
        {
            string jsonFilePath = "config.json"; 
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                checkBoxState = JsonConvert.DeserializeObject<CheckBoxState>(json);
                checkBox1.Checked = checkBoxState.CheckBox1Checked;
                checkBox2.Checked = checkBoxState.CheckBox2Checked;
                checkBox3.Checked = checkBoxState.CheckBox3Checked;
                checkBox5.Checked = checkBoxState.CheckBox5Checked;
                checkBox7.Checked = checkBoxState.CheckBox7Checked;
                checkBox8.Checked = checkBoxState.CheckBox8Checked;
                checkBox9.Checked = checkBoxState.CheckBox9Checked;
                checkBox10.Checked = checkBoxState.CheckBox10Checked;
            }
        }

        private void SaveCheckBoxState()
        {
            string jsonFilePath = "config.json"; 
            string json = JsonConvert.SerializeObject(checkBoxState);
            File.WriteAllText(jsonFilePath, json);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox1Checked = checkBox1.Checked;
            SaveCheckBoxState();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox2Checked = checkBox2.Checked;
            SaveCheckBoxState();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox3Checked = checkBox3.Checked;
            SaveCheckBoxState();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox5Checked = checkBox5.Checked;
            SaveCheckBoxState();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox7Checked = checkBox7.Checked;
            SaveCheckBoxState();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox8Checked = checkBox8.Checked;
            SaveCheckBoxState();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox9Checked = checkBox9.Checked;
            SaveCheckBoxState();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxState.CheckBox10Checked = checkBox10.Checked;
            SaveCheckBoxState();
        }
        private void Main_Load(object sender, EventArgs e)
        {

            if (Login.KeyAuthApp.user_data != null && !string.IsNullOrEmpty(Login.KeyAuthApp.user_data.username))
            {
                key.Text = "User: " + Login.KeyAuthApp.user_data.username;
            }
            else
            {
                key.Text = "User: Not Available";
            }

            expiry.Text = "Expiry: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry));
            subscription.Text = "Subscription: " + Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            ip.Text = "IP Address: " + Login.KeyAuthApp.user_data.ip;
            hwid.Text = "HWID: " + Login.KeyAuthApp.user_data.hwid;
            createDate.Text = "Creation date: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            lastLogin.Text = "Last login: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));
            subscriptionDaysLabel.Text = "Expiry in Days: " + expirydaysleft();


            LoadCheckBoxState();
        }







        private string expirydaysleft()
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry)).ToLocalTime();
            TimeSpan difference = dtDateTime - DateTime.Now;
            return Convert.ToString(difference.Days + " Days " + difference.Hours + " Hours Left");
        }

        private DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }

        private class CheckBoxState
        {
            public bool CheckBox1Checked { get; set; }
            public bool CheckBox2Checked { get; set; }
            public bool CheckBox3Checked { get; set; }
            public bool CheckBox5Checked { get; set; }
            public bool CheckBox7Checked { get; set; }
            public bool CheckBox8Checked { get; set; }
            public bool CheckBox9Checked { get; set; }
            public bool CheckBox10Checked { get; set; }

        }

        private void key_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load_2(object sender, EventArgs e)
        {

        }
    }
}