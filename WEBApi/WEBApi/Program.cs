using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*DrinkManager drinks = new DrinkManager();
            drinks.AddDrink("Italian coffee", 100, 2.50);
            drinks.AddDrink("American coffee", 90, 2.00);
            drinks.AddDrink("Tea", 100, 1.50);
            drinks.AddDrink("Chocolate", 100, 2.00);*/


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
