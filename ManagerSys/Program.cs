using LogDashboard;
using ManagerSys.Domain.Shared.ConfigHelper;
using ManagerSys.Domain.Shared.Middlewares;
using ManagerSys.Host;
using Serilog;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//await builder.AddApplicationAsync<HttpApiHostModule>();

////builder.Services.AddControllers();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
////await app.InitializeApplicationAsync();

////app.UseAuthorization();
//app.Run();
var builder = WebApplication.CreateBuilder(args);
builder.Host
    .AddAppSettingsSecretsJson()
    .UseAutofac();
//.UseSerilog();

#region  默认config
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
builder.Services.AddSingleton(new Config(configuration));
#endregion
await builder.AddApplicationAsync<HttpApiHostModule>();
//Serilog配置
SerilogMiddleware.Serilog(builder);
var app = builder.Build();

//使用Serilog可视化界面
app.UseLogDashboard();
await app.InitializeApplicationAsync();

try
{
    Log.Information("Starting ManagerSys.Host...");
    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}