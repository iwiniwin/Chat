using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using static System.Text.Encoding;
using Chat;
using System.Threading;

namespace Chat
{
    class ChatClient : ChatBase
    {
        public int TryConnectInterval = 2000;
        public override event ConnectEventHandler OnConnect;
        public ChatClient(string ip, int port) : base(ip, port) { }

        public override void Start()
        {
            StartConnect();
        }

        public void StartConnect()
        {
            Thread connectThread = new Thread(new ThreadStart(TryConnect));
            connectThread.IsBackground = true;
            connectThread.Start();
        }

        public void TryConnect()
        {
            Socket socket = GetSocket();
            while (true)
            {
                try
                {
                    Console.WriteLine("try connect ... ...");
                    socket.Connect(this.GetIPEndPoint());
                    // 如果连接上
                    this.ConnectedSocket = socket;
                    OnConnect();
                    this.StartReceive();
                    Console.WriteLine("connected ... ..." + socket.RemoteEndPoint.ToString());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("connect exception : " + e.Message);
                    Thread.Sleep(TryConnectInterval);
                }
            }
        }
    }
}
