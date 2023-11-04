using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_comisiones.Querys
{
    public class VentasQuery
    {
        private readonly ILogger<VentasQuery> _logger;

        public VentasQuery(ILogger<VentasQuery> logger)
        {
            this._logger = logger;
        }
        public string SP_VENTAS_A_GUARDIAN(DateTime FechaInicio, DateTime FechaFin)
        {
            this._logger.LogInformation($"VentasQuery/SP_VENTAS_A_GUARDIAN({FechaInicio.ToString("yyyyMMdd")},{FechaFin.ToString("yyyyMMdd")})");
            return $@"
                  EXEC BDQISHUR.dbo.SP_VENTAS_A_GUARDIAN '20230601','20230731';  
            ";
        }
        public string ObtenerCliente(int IDCLIENTE)
        {
            return @$"
                select
                    c.idcliente,
                    0 as lcontactoid,
                    rtrim(c.docid) as docid,
                    rtrim(ext.abreviado) abreviado, (
                        rtrim(ccn.nombre1) + ' ' + rtrim(ccn.nombre2)
                    ) as nombres,
                    rtrim(ccn.appaterno) as appaterno,
                    rtrim(ccn.apmaterno) aapmaterno,
                    rtrim(c.ruc) as ruc,
                    rtrim(c.direccion) as direccion,
                    rtrim(c.telefono) as telefono,
                    rtrim(c.fax) as fax,
                    rtrim(c.email) as email,
                    ccn.idciudad_residencia,
                    ccn.idpais_residencia,
                    1 as idpais_operacion,
                    c.idpaisnacimiento,
                    ISNULL(ciu.DESCRIPCION, 'Santa Cruz') as ciudadRecidencia,
                    ISNULL(cg.idPaisGuardian, 2) as idPaisGuardian,
                    c.fechanacimiento,
                    rtrim(c.sexo) as sexo,
                    rtrim(c.idestadocivil) as idestadocivil,
                    rtrim(u.usua_id) as usua_id,
                    rtrim(u.usua_password) as usua_password
                from
                    BDComisiones.dbo.grlcliente c
                    inner join BDComisiones.dbo.grlcliente_ccn ccn on c.idcliente = ccn.idcliente
                    left join BDComisiones.dbo.peciudad ext on ext.idciudad = c.idorigendocid
                    left join BDComisiones.dbo.gral_usuario u on u.usua_id = c.docid
                    left join BDComisiones.dbo.peciudad ciu on ciu.idciudad = ccn.idciudad_residencia
                    left join BDComisiones.dbo.PAISCONEXIONGUARDIAN cg on cg.idPaisConexion = ccn.IDPAIS_RESIDENCIA
                where c.idcliente = '{IDCLIENTE}'
            ";
        }
    }
}