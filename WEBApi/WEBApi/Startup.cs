using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WEBApi.Models;
using Microsoft.Extensions.Options;
using WEBApi.Services;
using WEBApi.Repositories.Drinks;
using WEBApi.Repositories.Orders;
using WEBApi.Repositories.DiscountCodes;

namespace WEBApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<IDrinksRepository>(x => new DrinksRepository(
                "mongodb://localhost:27017/MyDB",
                "Drinks")); 
            services.AddSingleton<IDiscountCodesRepository>(x => new DiscountCodesRepository(
                 "mongodb://localhost:27017/MyDB",
                 "DiscountCodes")); 
            services.AddSingleton<IOrdersRepository>(x => new OrdersRepository(
                 "mongodb://localhost:27017/MyDB",
                 "Orders"));

            // Services
            services.AddSingleton<IDrinkService, DrinkService>();
            services.AddSingleton<IDiscountCodeService, DiscountCodeService>();
            services.AddSingleton<IOrderService, OrderService>();

            services.AddControllers();
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection, IServiceProvider serviceProvider)
        {

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
