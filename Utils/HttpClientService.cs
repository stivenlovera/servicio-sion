using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace service_comisiones.Utils
{
    public class HttpClientService
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(
           ILogger<HttpClientService> logger
        )
        {
            this._logger = logger;
        }
        public async Task<Response> Run(string host, string url, EmailRequest emailRequest)
        {
            try
            {
                this._logger.LogInformation($"Preparando email {emailRequest.asunto}....");
                var stringContent = new StringContent(JsonConvert.SerializeObject(emailRequest), UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                HttpResponseMessage response = await client.PostAsync($"https://{host}/{url}", stringContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                this._logger.LogInformation($"Email enviado correctamente");
                return new Response()
                {
                    estado = true,
                    data = responseBody
                };
            }
            catch (HttpRequestException e)
            {
                this._logger.LogCritical($"Error al enviar email {e}");
                return new Response()
                {
                    estado = false,
                    data = "error"
                };
            }
        }
        public class EmailRequest
        {
            public string proyecto { get; set; } = "Comisiones Prueba";
            public string modulo { get; set; } = "Comisiones";
            public List<string> destinatarios { get; set; } = new List<string> { "alovera@gruposion.bo" };//"desarrollo@gruposion.bo" 
            public List<string> ccDestinatarios { get; set; } = new List<string> { "desarrollo@gruposion.bo" };
            public string asunto { get; set; } = "prueba servicio job";
            public string mensaje { get; set; }
        }
        public class Response
        {
            public bool estado { get; set; }
            public string data { get; set; }
        }
    }
}