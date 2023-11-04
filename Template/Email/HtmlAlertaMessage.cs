
using service_comisiones.Model;

namespace ServiceComisiones.PlantillaHtml
{
    public class HtmlAlertaMessage
    {
        public string MessageAlerta(EmailAlerta emailAlerta)
        {
            List<string> ips = new List<string>();

            System.Net.IPHostEntry entry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (System.Net.IPAddress ip in entry.AddressList)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ips.Add(ip.ToString());
            System.Console.WriteLine(ips[0]);
            string style = @"
                <style>
                    body {
                        font: Serif;
                        font-size: 13px;
                        text-rendering: geometricPrecision;
                        margin-top: 20px;
                    }

                    .centrado {
                        text-align: center;
                        font-size: 19px;
                        margin: 0px;
                        padding: 0px;
                    }

                    .titulo {
                        margin-left: 5%;
                        margin-right: 5%;
                    }

                    p {
                        padding-bottom: 0px;
                    }

                    p.seguido {
                        margin-top: 0px;
                        margin-bottom: 0px;
                    }

                    .container {
                        text-align: center;
                    }

                    .left {
                        float: left;
                    }

                    .right {
                        float: right;
                    }

                    .center {

                    }
                    table, td, th {
                        padding: 5px;
                        border: 1px solid;
                    }

                    table {
                        border-collapse: collapse;
                    }
                </style>
            ";

            string resultado = $@"";

            /* foreach (var value in ventaGrupoMessage.VentaGrupos)
            {
                resultado += $@"<tr>
                    <td> {value.lcontact_id} </td>
                    <td> {value.nombreCompleto} </td>
                    <td> {value.precioPlanMontesion} </td>
                    <td> {value.precioVentaGrupo} </td>
                    <td> {value.status} </td>
                    <td> {value.cantidad} </td>
                </tr>";
            } */

            return $@"
                <!DOCTYPE html>
                <html lang='es'>

                <head>
                    <meta charset='UTF-8'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Document</title>
                   {style}
                    
                </head>
                
                <body>
                    <h2>Alerta de Prueba, ventas por grupo</h2>
                    <p>Los siguientes resultados son de la de TABLA 't_plan_montesion' comparados con TABLA 
                        'administracionventagrupo' y su cantidad lotes vendidos</p>
                    <table>
                        <thead>
                            <tr>
                                <th>lcontacto_id</th>
                                <th>Nombre</th>
                                <th>Venta Grupo(t_plan_montesion)</th>
                                <th>Venta Grupo(administracionventagrupo)</th>
                                <th>Status</th>
                                <th>Cantidad</th>
                            </tr>
                        </thead>
                        <tbody>
                            {resultado}
                        </tbody>
                    </table>
                </body>

                </html>
            ";
        }
    }
}
