using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using static System.Text.Encoding;
using System.Net;
using System.IO;

namespace Chat
{
    public delegate void OnReceiveEventHandler(ChatType type, string msg);
    public delegate void DisconnectEventHandler(Exception e);
    public delegate void ConnectEventHandler();

    public enum ChatType : byte {
        Str,
        File,
    }


    abstract class ChatBase
    {
        public Socket ConnectedSocket { get; set; }
        public bool AutoReConnect { get; set; } = true;
        public event OnReceiveEventHandler OnReceive;
        public event DisconnectEventHandler OnDisconnect;
        public abstract event ConnectEventHandler OnConnect;
        private Socket socket;

        public string ip;
        public int port;

        public ChatBase(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public abstract void Start();

        public IPEndPoint GetIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(ip), port);
        }

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
            if (ConnectedSocket != null && ConnectedSocket.Connected)
            {
                byte[] buffer = UTF8.GetBytes(msg);
                byte[] len = BitConverter.GetBytes((long)buffer.Length);
                byte[] content = new byte[1 + len.Length + buffer.Length];
                content[0] = (byte)ChatType.Str;
                Array.Copy(len, 0, content, 1, len.Length);
                Array.Copy(buffer, 0, content, 1 + len.Length, buffer.Length);

                try
                {
                    ConnectedSocket.Send(content);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "   ooo");
                }
            }
            return false;
        }

        public bool SendFile(string path)
        {
            if (ConnectedSocket != null && ConnectedSocket.Connected)
            {
                try
                {
                    FileInfo fi = new FileInfo(path);
                    byte[] len = BitConverter.GetBytes(fi.Length);
                    byte[] name = UTF8.GetBytes(fi.Name);
                    byte[] nameLen = BitConverter.GetBytes(name.Length);
                    byte[] head = new byte[1 + len.Length + nameLen.Length + name.Length];
                    head[0] = (byte)ChatType.File;
                    Array.Copy(len, 0, head, 1, len.Length);
                    Array.Copy(nameLen, 0, head, 1 + len.Length, nameLen.Length);
                    Array.Copy(name, 0, head, 1 + len.Length + nameLen.Length, name.Length);
                    ConnectedSocket.SendFile(
                        path,
                        head,
                        null,
                        TransmitFileOptions.UseDefaultWorkerThread
                    );
                    return true;
                }
                catch(Exception e)
                {
                    // 连接断开了
                    Console.WriteLine("send file exception : " + e.Message);
                }
                
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
                        byte[] head = new byte[9];
                        ConnectedSocket.Receive(head, head.Length, SocketFlags.None);

                        int len = BitConverter.ToInt32(head, 1);
                        if (head[0] == (byte) ChatType.Str)
                        {
                            byte[] buffer = new byte[len];
                            ConnectedSocket.Receive(buffer, len, SocketFlags.None);
                            OnReceive(ChatType.Str, UTF8.GetString(buffer));
                        }
                        else if(head[0] == (byte)ChatType.File)
                        {
                            byte[] nameLen = new byte[4];
                            ConnectedSocket.Receive(nameLen, nameLen.Length, SocketFlags.None);

                            byte[] name = new byte[BitConverter.ToInt32(nameLen, 0)];
                            ConnectedSocket.Receive(name, name.Length, SocketFlags.None);
                            
                            string fileName = UTF8.GetString(name);

                            int readByte = 0;
                            int count = 0;
                            byte[] buffer = new byte[1024 * 8];
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                            {
                                while (count != len)
                                {
                                    int readLength = buffer.Length;
                                    if(len - count < readLength)
                                    {
                                        readLength = len - count;
                                    }
                                    readByte = ConnectedSocket.Receive(buffer, readLength, SocketFlags.None);
                                    fs.Write(buffer, 0, readByte);
                                    count += readByte;
                                }
                            }
                            OnReceive(ChatType.File, fileName);
                        }
                        else
                        {
                            // 未知类型
                        }
                    }
                    catch (Exception e)
                    {
                        OnDisconnect(e);
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
