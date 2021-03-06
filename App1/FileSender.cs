using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace App1
{
    public class FileSender
    {
        private TcpListener Server;
        private Settings AppSettings;

        public FileSender(Settings appSettings)
        {
            AppSettings = appSettings;
        }

        public void SendFile(string filename)
        {
            Console.WriteLine("wait connection...");

            var sshConnector = new SshConnector(AppSettings);

            TcpClient client = Server.AcceptTcpClient();

            Console.WriteLine("client connected");

            var stream = client.GetStream();
            byte[] filenameBytes = Encoding.UTF8.GetBytes(Path.GetFileName(filename));
            stream.Write(filenameBytes, 0, filenameBytes.Length);

            Thread.Sleep(200);

            var fileStreamSource = File.OpenRead(filename);
            fileStreamSource.CopyTo(stream);
            stream.Close();
            fileStreamSource.Close();
        }

        public void StartTcpServer()
        {
            IPAddress localAddr = IPAddress.Parse(AppSettings.IpAddressLocal);
            Server = new TcpListener(localAddr, AppSettings.Port);
            Server.Start();

            Console.WriteLine("tcp server started");
        }
    }
}
