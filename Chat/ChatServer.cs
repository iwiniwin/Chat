using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Chat;

namespace Chat
{
    class ChatServer : ChatBase
    {
        public override event ConnectEventHandler OnConnect;
        public ChatServer(string ip, int port) : base(ip, port) {

            string localIP = string.Empty;
            foreach(IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if(address.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = address.ToString();
                }
            }

            this.ip = localIP;
        }

        public override void Start()
        {
            StartListen();
        }

        public void StartListen()
        {
            Socket socket = GetSocket();
            // 将套接字与IPEndPoint绑定
            socket.Bind(this.GetIPEndPoint());
            // 开启监听 仅支持一个连接
            socket.Listen(1);

            Thread acceptThread = new Thread(new ThreadStart(TryAccept));
            acceptThread.IsBackground = true;
            acceptThread.Start();
        }

        public void TryAccept()
        {
            Socket socket = GetSocket();
            while (true)
            {
                try
                {
                    Socket connectedSocket = socket.Accept();

                    this.ConnectedSocket = connectedSocket;
                    OnConnect();
                    this.StartReceive();
                    break;
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
