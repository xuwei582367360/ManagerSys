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
await builder.AddApplicationAsync<HttpApiHostModule>();
#region  Ĭ��config
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
builder.Services.AddSingleton(new Config(configuration));
#endregion
//Serilog����
SerilogMiddleware.Serilog(builder);
var app = builder.Build();

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