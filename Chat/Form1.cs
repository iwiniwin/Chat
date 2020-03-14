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
    public enum ChatMode
    {
        Server,
        Client
    }
    public partial class Form1 : Form
    {
        public ChatMode mode = ChatMode.Server;

        public string localName = "local";
        public string remoteName = "remote";
        public string ip = "127.0.0.1";
        public int port = 30;
        private ChatBase chat;

        public Form1()
        {
            InitializeComponent();
            
            if (mode == ChatMode.Server)
            {
                chat = new ChatServer(ip, port);
                this.Text = "S-Chat";
            }
            else
            {
                chat = new ChatClient(ip, port);
                this.Text = "C-Chat";
            }
            
            chat.OnReceive += delegate (string msg)
            {
                this.charContentRichText.Invoke(new OnReceiveEventHandler(OnReceiveMsg), msg);
            };
            chat.Start();
        }

        public void OnReceiveMsg(string msg)
        {
            SetTextValue(remoteName, msg);
        }

        public void SetTextValue(string name, string msg)
        {
            this.charContentRichText.AppendText(name + " : \n" + msg + "\n");
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string msg = this.sendTextBox.Text;
            if (msg != "")
            {
                bool ret = chat.Send(msg);
                if (ret) 
                    this.sendTextBox.Text = "";
                    SetTextValue(localName, msg);
            }
        }
    }
}
