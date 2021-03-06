using System;
using System.Collections.Generic;
using System.Text;
using Renci.SshNet;
using System.Threading.Tasks;

namespace App1
{
    public class SshConnector
    {
        public SshConnector(Settings settings)
        {
            Console.WriteLine("Start ssh connect!");

            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo(
                settings.IpAddressApp2, 
                settings.UserSSH, 
                settings.PasswordSSH);
            
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);

            using (var client = new SshClient(connectionInfo))
            {
                try
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        SshCommand sc = client.CreateCommand($"cd {settings.App2Location} && dotnet run");
                        sc.BeginExecute();

                        Console.WriteLine("ssh command sended");
                    }
                    else
                    {
                        Console.WriteLine("SSH connection NOTactive");
                    }
                }
                catch { }
            }
        }
    }
}
