using Final.EFW.Database;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using NLog;
using NLog.Config;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Final
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Debug("init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddControllersWithViews();
                builder.Services.AddDbContext<Core.ApplicationContext>(option => option.UseSqlite("Data Source=../Database/Final.db"));
                //builder.Services.AddDbContext<DbContext>(option => option.UseSqlite("Data Source=../Database/Final.db"));

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                }
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
