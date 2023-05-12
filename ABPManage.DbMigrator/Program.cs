using ABPManage.DbMigrator;
using Autofac.Extensions.DependencyInjection;
using ManagerSys.Domain.Shared.ConfigHelper;
using ManagerSys.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

class Program
{
    static async Task Main(string[] args)
    {


        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("BookStore", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("BookStore", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        await CreateHostBuilder(args).RunConsoleAsync();


    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(build =>
            {
                build.AddJsonFile("appsettings.secrets.json", optional: true);
            }).UseServiceProviderFactory(new AutofacServiceProviderFactory()) //使用autofac的容器工厂替换系统默认的容器
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<DbMigratorHostedService>();
            });
}
