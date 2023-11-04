using Microsoft.AspNetCore.Hosting;
using service_comisiones.Model;
using service_comisiones.Utils;
using ServiceComisiones.PlantillaHtml;
using static service_comisiones.Utils.HttpClientService;

namespace service_comisiones;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly HtmlAlertaMessage _htmlAlertaMessage;
    private readonly HttpClientService _httpClientService;

    public Worker(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        HtmlAlertaMessage htmlAlertaMessage,
        HttpClientService httpClientService
    )
    {
        this._logger = logger;
        this._serviceProvider = serviceProvider;
        this._configuration = configuration;
        this._htmlAlertaMessage = htmlAlertaMessage;
        this._httpClientService = httpClientService;
    }
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("SERVICIO-PRUEBA | INICIANDO...");
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("SERVICIO-PRUEBA | DETENIDO...");
        await base.StopAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var email = new EmailAlerta();
            email.descripcion = "esto es una descripcion";
            email.nombre = "nombreee";

            var templates = this._configuration.GetSection("Templates").Get<Templates>();

            var plantilla = this._htmlAlertaMessage.MessageAlerta(email);
            EmailRequest emailRequest = new EmailRequest()
            {
                proyecto = "COMISIONES",
                modulo = "Prueba servicio",
                asunto = "Prueba de servicio",
                ccDestinatarios = new List<string> { "alovera@gruposion.bo" },
                destinatarios = new List<string> { "alovera@gruposion.bo" },
                mensaje = plantilla
            };
            var response = await this._httpClientService.Run("correoservice.gruposion.bo", "api/mensajeria/EnvioCorreo", emailRequest);
            this._logger.LogInformation($"Respuesta del servicio de mensajeria {response.data}");
            this._logger.LogInformation("SERVICIO-PRUEBA running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000 * 60 * 15, stoppingToken);
        }
    }
}
