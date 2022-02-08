using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Acrelec.SCO.Server
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("This is the SCO Server");

            CreateHostBuilder(args).Build().Run();

            //todo - implement REST server accepting the 2 API methods
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              });
    }
}
