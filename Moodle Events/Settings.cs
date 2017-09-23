using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodle_Events
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Account_SID = acc_sid.Text;
            Properties.Settings.Default.Auth_Token = auth_token.Text;
            Properties.Settings.Default.Twilio_Number = twil_number.Text;
            Properties.Settings.Default.Cell_Number = cell_number.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Account_SID = acc_sid.Text;
            Properties.Settings.Default.Auth_Token = auth_token.Text;
            Properties.Settings.Default.Twilio_Number = twil_number.Text;
            Properties.Settings.Default.Cell_Number = cell_number.Text;
            Properties.Settings.Default.Save();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            acc_sid.Text = Properties.Settings.Default.Account_SID;
            auth_token.Text = Properties.Settings.Default.Auth_Token;
            twil_number.Text = Properties.Settings.Default.Twilio_Number;
            cell_number.Text = Properties.Settings.Default.Cell_Number;
        }
    }
}
