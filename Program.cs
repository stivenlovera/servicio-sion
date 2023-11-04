
using System.Reflection;
using Serilog;
using service_comisiones;

//loguen en entorno produccion /Bin
System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

//serilog configuracion
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Error)
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Debug)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/Log-.log", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u15}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args);

var statup = new Startup(host);
statup.ConfigureServices(host);
host.UseSerilog(Log.Logger);
host.UseWindowsService();
var app = host.Build();

try
{
    Log.Information("SERVICIO DE COMISIONES");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return;
}



