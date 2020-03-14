using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using static System.Text.Encoding;
using System.Net;

namespace Chat
{
    public delegate void OnReceiveEventHandler(string msg);
    abstract class ChatBase
    {
        public Socket ConnectedSocket { get; set; }
        public bool AutoReConnect { get; set; } = true;
        public IPEndPoint ConnectIPEndPoint { get; set; }
        public event OnReceiveEventHandler OnReceive;
        private Socket socket;

        public ChatBase(string ip, int port)
        {
            ConnectIPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public abstract void Start();

        public Socket GetSocket()
        {
            if(socket == null)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            return socket;
        }

        public bool Send(string msg)
        {
            if(ConnectedSocket != null && ConnectedSocket.Connected)
            {
                byte[] buffer = UTF8.GetBytes(msg);
                ConnectedSocket.Send(buffer);
                return true;
            }
            return false;
        }

        public void StartReceive()
        {
            Thread receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void Receive()
        {
            if (ConnectedSocket != null)
            {
                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        int size = ConnectedSocket.Receive(buffer);
                        if (size > 0)
                        {
                            string msg = UTF8.GetString(buffer, 0, size);
                            // 接收到消息
                            //Console.WriteLine("receive : " + msg);
                            OnReceive(msg);
                        }
                    }
                    catch (Exception e)
                    {
                        // 连接异常断开
                        if (AutoReConnect)
                        {
                            this.socket.Close();
                            this.socket = null;
                            this.Start();
                        }
                        break;
                    }
                }
            }
        }
    }
}
