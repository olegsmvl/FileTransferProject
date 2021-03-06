using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Settings
    {
        public string Folder { get; set; }
        public string IpAddressLocal { get; set; }
        public int Port { get; set; }
        public string IpAddressApp2 { get; set; }
        public string UserSSH { get; set; }
        public string PasswordSSH { get; set; }

        public string App2Location { get; set; }
    }
}
