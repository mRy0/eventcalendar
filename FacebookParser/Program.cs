using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EventCalendar
{
    public class Program
    {
        public static void Main(string[] args)
        {
           new CalendarDownloader.CalendarManager();

            
            CreateWebHostBuilder(args).Build().Run();

            CalendarDownloader.CalendarManager.Instance.Dispose();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
