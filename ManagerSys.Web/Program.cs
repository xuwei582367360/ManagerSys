
using LogDashboard;
using ManagerSys.Domain.Shared.ConfigHelper;
using ManagerSys.Domain.Shared.Middlewares;
using ManagerSys.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .AddAppSettingsSecretsJson()
    .UseAutofac();
//.UseSerilog();
builder.Services.AddRazorPages();
#region  Ĭ��config
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
builder.Services.AddSingleton(new Config(configuration));
#endregion
await builder.AddApplicationAsync<HttpApiHostModule>();
//Serilog����
SerilogMiddleware.Serilog(builder);
var app = builder.Build();
app.UseStaticFiles();
//ʹ��Serilog���ӻ�����
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