using service_comisiones.Modules;

namespace service_comisiones;

public class VentaService : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public VentaService(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider
        )
    {
        this._logger = logger;
        this._serviceProvider = serviceProvider;
    }
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("VENTA SERVICE");
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("VENTA SERVICE interrumpido");
        await base.StopAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    await scope.ServiceProvider.GetRequiredService<VentasModule>().Index();
                }
            }
            catch (System.Exception ex)
            {
                this._logger.LogCritical($"Error generado {ex.Message}");
            }
            this._logger.LogInformation("VENTA SERVICE  running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000 * 60 * 1, stoppingToken);
        }
    }
}
