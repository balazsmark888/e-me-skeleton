using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace e_me.server.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://localhost:5001",
                        $"https://{Dns.GetHostEntry(Dns.GetHostName()).AddressList.Last()}:5001");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
