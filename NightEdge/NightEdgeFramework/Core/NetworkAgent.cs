using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using NightEdgeFramework.Network;
using System.IO;
using System.Threading;

namespace NightEdgeFramework.Core
{
    public class NetworkAgent
    {
        private static NetworkAgent networkAgent;
        private NefxNetSolver solver;

        const string REQUEST = "GET /index HTTP/1.1";

        #region Initialize
        private NetworkAgent()
        {
            Initialize();
        }

        private void Initialize()
        {
            solver = NefxNetSolver.GetNefsNetSolver();
        }

        public static NetworkAgent GetNetworkAgent()
        {
            if (networkAgent == null)
            {
                networkAgent = new NetworkAgent();
            }
            return networkAgent;
        }
        #endregion

        #region temp

        public void PrintLocalHost()
        {
            IPAddress ip = IPAddress.Parse("81.70.164.120");
            IPEndPoint ep = new IPEndPoint(ip, 22);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(ep);
            System.Console.WriteLine(s.RemoteEndPoint);

            s.Send(Encoding.UTF8.GetBytes(REQUEST));

            byte[] buffer = new byte[2 * 1024 * 1024];

            s.Receive(buffer, SocketFlags.None);
            System.Console.WriteLine(Encoding.UTF8.GetString(buffer));
        }

        #endregion


        #region Server

        public void LaunchServer()
        {
            string targetDirectory = Directory.GetCurrentDirectory() + @"\output";
            string targetName = "temp.pdf";
            string tempPath = Directory.GetCurrentDirectory() + @"\temp\";
            int p_size = (int)1e5;
            int count = 0;

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);
            //if (!File.Exists(targetDirectory + "\\" + targetName))
            //    File.Create(targetDirectory + "\\" + targetName);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 5000);
            s.Bind(ep);
            System.Console.WriteLine("监听成功...");
            s.Listen(10);


            Thread thread = new Thread(() =>
            {
                Socket socketSend = s.Accept();
                System.Console.WriteLine(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功！");

                while (true)
                {
                    // 发送开始传送信号
                    socketSend.Send(new byte[] { 1 });
                    byte[] buffer = new byte[p_size];
                    int len = socketSend.Receive(buffer);
                    using (var fsWrite = new FileStream(tempPath + $"{count}.nefstmp", FileMode.Create))
                    {
                        fsWrite.Write(buffer, 0, len);
                    }

                    // 发送完成传送信号
                    socketSend.Send(new byte[] { 1 });
                    byte[] flag = new byte[1];
                    socketSend.Receive(flag, SocketFlags.None);
                    if (flag[0] == 0)
                        break;
                    count++;
                }

                var fa = FileAgent.GetFileAgent();
                fa.UnpackFiles(targetName, targetDirectory, tempPath, p_size);
            });

            thread.Start(); 
        }


        public void RecieveFile()
        {

        }

        #endregion

        #region Client

        public void LaunchClient()
        {

        }

        public void Connect()
        {

        }

        public void Disconnect()
        {

        }

        public void TransferFile()
        {
            string targetDirectory = Directory.GetCurrentDirectory()+"\\";
            string targetName = "temp.pdf";
            string tempPath = Directory.GetCurrentDirectory() + @"\temp\";
            int p_size = (int)1e5;
            int count = 0;

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            System.Console.WriteLine("创建目录成功！");

            var fa = FileAgent.GetFileAgent();
            fa.PackFiles(targetName, targetDirectory, tempPath, p_size);

            System.Console.WriteLine("打包完成");

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.1.2"), 5000);

            s.Connect(ep);
            System.Console.WriteLine("连接成功");

            string[] paths = Directory.GetFiles(tempPath);

            foreach (var item in paths)
            {
                // 接收到信号后开始传送
                s.Receive(new byte[p_size]);

                using (var fsReader = new FileStream(item, FileMode.Open))
                {
                    byte[] buffer = new byte[p_size];
                    int len = fsReader.Read(buffer, 0, p_size);
                    s.Send(buffer);
                    System.Console.WriteLine("发送成功！");
                }
                // 接收到信号后完成传送
                s.Receive(new byte[p_size]);
                count++;
                if(count >= paths.Length)
                {
                    s.Send(new byte[] { 0 });
                }
                else
                {
                    s.Send(new byte[] { 1 });
                }
            }

            System.Console.ReadKey();
            
        }

        #endregion

    }
}
