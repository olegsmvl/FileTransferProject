using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace App1
{
    class Program
    {
        static Settings AppSettings;

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();

            Thread.Sleep(100);

            var fileSender = new FileSender(AppSettings);
            fileSender.StartTcpServer();

            var fileWatcher = new FileWatcher(AppSettings);
            fileWatcher.FileEvent += (filename) => fileSender.SendFile(filename);
            fileWatcher.Start();

            while(true)
                Thread.Sleep(1);
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

                Console.WriteLine($"Settings.Folder={AppSettings.Folder }");
                Console.WriteLine($"Settings.IpAddressApp2={AppSettings.IpAddressApp2 }");
            });


    }
}
