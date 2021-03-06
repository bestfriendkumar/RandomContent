﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RandomContent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://localhost:44370")
                .Build();

        //public static IWebHost BuildWebHost(string[] args)
        //{
        //    var port = Environment.GetEnvironmentVariable("PORT");

        //    return WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .UseUrls("http://*:" + port)
        //        .Build();
        //}
    }
}
