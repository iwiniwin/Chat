using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class Form2 : Form
    {
        public SettingEventHandler handler;
        public Form2(SettingEventHandler handler, string localName, string remoteName, string ip)
        {
            this.handler = handler;
            InitializeComponent();
            this.localTextBox.Text = localName;
            this.remoteTextBox.Text = remoteName;
            this.ipTextBox.Text = ip;
        }

        public void SetIpEditEnable(bool enable)
        {
            this.ipTextBox.Enabled = enable;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.handler(this.localTextBox.Text, this.remoteTextBox.Text, this.ipTextBox.Text);
            this.Close();
        }
    }
}
