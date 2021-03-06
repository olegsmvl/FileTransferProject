using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace App2
{
    class Program
    {
        static Settings AppSettings;

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();

            Thread.Sleep(100);

            var fileSaver = new FileSaver(AppSettings);
            fileSaver.Save();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, configuration) =>
        {
            configuration.Sources.Clear();

            IHostEnvironment env = hostingContext.HostingEnvironment;

            configuration
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configurationRoot = configuration.Build();

            AppSettings = new Settings();
            configurationRoot.GetSection(nameof(Settings))
                                     .Bind(AppSettings);

            Console.WriteLine($"Settings.IpAddressApp1={AppSettings.IpAddressApp1}");
            Console.WriteLine($"Settings.Port={AppSettings.Port }");
            Console.WriteLine($"Settings.Folder={AppSettings.Folder}");
        });
    }
}
