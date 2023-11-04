using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using service_comisiones.Context;
using service_comisiones.Entities.DBComisiones;
using service_comisiones.Entities.DBComisiones.BDQISHUR;
using service_comisiones.Entities.DBGgrSion;
using service_comisiones.Querys;
using service_comisiones.Repository;

namespace service_comisiones.Modules
{
    public class VentasModule
    {
        private readonly ILogger<VentasModule> _logger;
        private readonly DbComisionesContext _dbComisionesContext;
        private readonly DbGrdSionContext _dbGrdSionContext;
        private readonly AdministracionContactoRepository _administracionContactoRepository;
        private readonly VentasQuery _ventasQuery;

        public VentasModule(
            ILogger<VentasModule> logger,
            DbComisionesContext DbComisionesContext,
            DbGrdSionContext dbGrdSionContext,
            AdministracionContactoRepository administracionContactoRepository,
            VentasQuery ventasQuery
        )
        {
            this._logger = logger;
            this._dbComisionesContext = DbComisionesContext;
            this._dbGrdSionContext = dbGrdSionContext;
            this._administracionContactoRepository = administracionContactoRepository;
            this._ventasQuery = ventasQuery;
        }
        /*Variables de para verificar*/
        private int ventasUpgrade = 0;
        private int ventaSinPatrocinador = 0;
        public async Task<List<SP_VENTAS_A_GUARDIAN>> Index()
        {
            this._logger.LogInformation($"VentasModule/Index() iniciando...");
            var ventas = await this._dbComisionesContext.SP_VENTAS_A_GUARDIAN.FromSqlRaw(
                this._ventasQuery.SP_VENTAS_A_GUARDIAN(DateTime.Now, DateTime.Now)
                ).ToListAsync();
            this._logger.LogInformation($"VentasModule/Index() SUCCESS=> {JsonConvert.SerializeObject(ventas, Formatting.Indented)} ");

            await this.VerificarVentas(ventas);
            return ventas;
        }
        private async Task<string> VerificarVentas(List<SP_VENTAS_A_GUARDIAN> ventas)
        {
            this._logger.LogInformation($"VentasModule/VerificarVentas() Iniciando...");
            foreach (var venta in ventas)
            {
                var isUpgrade = this.VerificarSiUpgrade(venta, "UPGRADE");
                if (!isUpgrade)
                {
                    var patrocinador = await this.VerificarSinPatrocinador(venta);

                    await this.VerificarExisteCliente(venta, patrocinador);

                }
            }
            return "veri";
        }
        /*VERIFICACIONES*/
        private bool VerificarSiUpgrade(SP_VENTAS_A_GUARDIAN venta, string upgrade)
        {
            bool resultado = venta.GLOSA.Contains("UPGRADE");
            if (resultado) //retorna true si existe en la glosa
            {
                //añadir al contrado de excluciones
                ventasUpgrade++;
            }
            this._logger.LogInformation($"VentasModule/VerificarSiUpgrade({upgrade}) resultado => {resultado}");
            return resultado;
        }
        private async Task<AdministracionContacto> VerificarSinPatrocinador(SP_VENTAS_A_GUARDIAN venta)
        {
            var resultado = await this._dbGrdSionContext.administracioncontacto.Where(x => x.scedulaidentidad == venta.CI_VENDEDOR).FirstOrDefaultAsync();
            if (resultado == null) //retorna true si existe patrocinador
            {
                //añadir al contrado de excluciones
                ventaSinPatrocinador++;
            }
            this._logger.LogInformation($"VentasModule/VerificarSinPatrocinador() resultado => {resultado}");
            return resultado;
        }
        private async Task<bool> VerificarExisteCliente(SP_VENTAS_A_GUARDIAN venta, AdministracionContacto patrocinador)
        {
            var cliente = await this._dbGrdSionContext.administracioncontacto.Where(x => x.scedulaidentidad == venta.CI_CLIENTE).FirstOrDefaultAsync();
            if (cliente == null) //si no existe se crea cliente
            {
                var sqlQuery = this._ventasQuery.ObtenerCliente(Convert.ToInt32(venta.IDCLIENTE));
                var datosClienteVenta = await this._dbComisionesContext.obtenerclientes.FromSqlRaw(sqlQuery).FirstOrDefaultAsync();
                this._logger.LogInformation($"VentasModule/VerificarExisteCliente() Cliente encontrado => {JsonConvert.SerializeObject(datosCliente, Formatting.Indented)}");
                await this.InsertarCliente(datosClienteVenta, patrocinador);
            }
            else //si existe
            {
                //verificar patrocinador


            }
            this._logger.LogInformation($"VentasModule/VerificarExisteCliente() resultado => {resultado}");
            return cliente;
        }

        /*Insercion*/
        public async Task<AdministracionContacto> InsertarCliente(ObtenerCliente obtenerCliente, AdministracionContacto patrocinador)
        {
            var generateLcontactoId = await this._administracionContactoRepository.GetUltimateLcontacto();
            var generarCodigo = await this._administracionContactoRepository.GetUltimoCodigo();
            var nuevo = new AdministracionContacto()
            {
                //bifoto = "",
                cbaja = "0",
                ccena = "0",
                ccorreo = "0",
                cestado = "0",
                cperiodico = "0",
                cpresentacion = "0",
                cpresentafactura = "0",
                cradio = "0",
                ctienecuenta = "0",
                ctv = "0",
                cvolante = "0",
                cweb = "0",
                ddescuentolote = 0M,
                dlotes = 0M,
                dproduccion = 0M,
                dtfechaadd = DateTime.Now,
                dtfechabaja = Convert.ToDateTime("1900-01-01"),
                dtfechacalculo = Convert.ToDateTime("1900-01-01"),
                dtfechamod = DateTime.Now,
                dtfechanacimiento = Convert.ToDateTime(obtenerCliente.fechanacimiento),
                dtfecharegistro = DateTime.Now,
                lcodigobanco = 0,
                lcontacto_id = generateLcontactoId.lcontacto_id + 1,
                lcuentabanco = "0",
                lnit = Convert.ToInt32(obtenerCliente.ruc),
                lnivel_id = 1,
                lpais_id = obtenerCliente.idPaisGuardian,
                lpatrocinante_id = (patrocinador == null) ? 1 : patrocinador.lcontacto_id, // si no tiene patrocinador por defecto pone 1 igual a sion
                lpatrotemp_id = 0,
                ltipobaja = 0,
                ltipocontacto_id = 1, //nuevo cliente
                pmax = 0,
                pvitalicio = 0,
                scedulaidentidad = obtenerCliente.docid,
                sciudad = obtenerCliente.ciudadRecidencia,
                scodigo = (generarCodigo) == null ? "0" : ((Convert.ToInt32(generarCodigo.scodigo) + 1).ToString()), //buscar el ultimo codigo caso contrario pone 0
                scontrasena = "",
                scorreoelectronico = obtenerCliente.email,
                sdireccion = obtenerCliente.direccion,
                smotivobaja = "",
                snombrecompleto = $"{obtenerCliente.appaterno.Trim()} {obtenerCliente.aapmaterno.Trim()} {obtenerCliente.nombres.Trim()}",
                snotadescuentolote = "",
                sotro = "",
                stelefonofijo = obtenerCliente.telefono,
                stelefonomovil = obtenerCliente.telefono,
                stelefonooficina = obtenerCliente.telefono,
                susuarioadd = "IntegracionVta",
                susuariomod = "IntegracionVta",
            };
            return await this._administracionContactoRepository.Store(nuevo);
        }
        public string IsDoblePatrocinio(AdministracionContacto cliente,SP_VENTAS_A_GUARDIAN venta)
        {

            return System.NotImplementedException;
        }
    }
}