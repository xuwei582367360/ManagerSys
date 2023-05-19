using LogDashboard;
using LogDashboard.Authorization.Filters;
using ManagerSys.Domain.Shared.ConfigHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace ManagerSys.Domain.Shared.Middlewares
{
    public class SerilogMiddleware
    {
        private readonly IConfiguration _Configuration;

        public SerilogMiddleware(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public static void Serilog(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                string errorFilePath = Path.Combine(AppContext.BaseDirectory, @$"Logs\{DateTime.Now.ToString("yyyyMMdd")}\Error", "Error.log");
                string infoFilePath = Path.Combine(AppContext.BaseDirectory, @$"Logs\{DateTime.Now.ToString("yyyyMMdd")}\Info", "Info.log");
                string warnFilePath = Path.Combine(AppContext.BaseDirectory, @$"Logs\{DateTime.Now.ToString("yyyyMMdd")}\Warn", "Warn.log");
                string debugFilePath = Path.Combine(AppContext.BaseDirectory, @$"Logs\{DateTime.Now.ToString("yyyyMMdd")}\Debug", "Debug.log");
                //Log模板不可更改
                var outputTemplate = "{Timestamp:HH:mm} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}";
                loggerConfiguration
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Default", LogEventLevel.Debug)
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Error)
                //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                //.WriteTo.Async(c => c.File(logFilePath, rollingInterval:RollingInterval.Day, outputTemplate: outputTemplate))
                .WriteTo.Logger(
                    l =>
                        l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                        .WriteTo.Async(c => c.File(errorFilePath, rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate))
                ).WriteTo.Logger(
                    l =>
                        l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                        .WriteTo.Async(c => c.File(infoFilePath, rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate))
                ).WriteTo.Logger(
                    l =>
                        l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                        .WriteTo.Async(c => c.File(warnFilePath, rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate))
                ).WriteTo.Logger(
                    l =>
                        l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                        .WriteTo.Async(c => c.File(debugFilePath, rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate))
                )
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            ;
            });
            //builder.Host.UseSerilog((context, logger) =>
            //{
            //    logger.ReadFrom.Configuration(context.Configuration);
            //    logger.Enrich.FromLogContext();
            //});

            //Serilog可视化界面，运行项目导航到/logdashboard
            builder.Services.AddLogDashboard(options =>
            {

                options.AddAuthorizationFilter(new LogDashboardBasicAuthFilter(
                    Config.ReadAppSettings("LogDashboard", "admin"), Config.ReadAppSettings("LogDashboard", "password")));
            });
        }
    }
}
