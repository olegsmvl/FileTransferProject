using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace App2
{
    public class FileSaver
    {
        private Settings AppSettings;
        public FileSaver(Settings settings)
        {
            AppSettings = settings;
        }
        public void Save()
        {
            string address = AppSettings.IpAddressApp1;
            int port = AppSettings.Port;

            var directory = AppSettings.Folder;

            var client = new TcpClient();
            client.Connect(address, port);

            var stream = client.GetStream();

            byte[] dataFilename = new byte[1024];

            var filename = String.Empty;

            do
            {
                int bytes = stream.Read(dataFilename, 0, dataFilename.Length);
                filename += Encoding.UTF8.GetString(dataFilename, 0, bytes);

            }
            while (stream.DataAvailable);

            var writePath = Path.Combine(directory, filename);
            String text = string.Empty;

            using (StreamReader sr = new StreamReader(stream, true))
            {
                text = sr.ReadToEnd();
                Console.WriteLine(text);
                Console.WriteLine("Source encoding: {0}.", sr.CurrentEncoding.EncodingName);
            }

            stream.Close();

            var converter = new Converter();

            using (StreamWriter sw = converter.Convert(text, writePath))
            {
                sw.WriteLine(text);
            }

            client.Close();
        }
    }
}
