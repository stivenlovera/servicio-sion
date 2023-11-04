using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace service_comisiones.Entities.DBComisiones
{
    [Keyless]
    public class ObtenerCliente
    {
        public decimal idcliente { get; set; }
        public int lcontactoid { get; set; }
        public string docid { get; set; }
        public string abreviado { get; set; }
        public string nombres { get; set; }
        public string appaterno { get; set; }
        public string aapmaterno { get; set; }
        public string ruc { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public int idciudad_residencia { get; set; }
        public int idpais_residencia { get; set; }
        public int idpais_operacion { get; set; }
        public int idpaisnacimiento { get; set; }
        public string ciudadRecidencia { get; set; }
        public int idPaisGuardian { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string sexo { get; set; }
        public string idestadocivil { get; set; }
        public string usua_id { get; set; }
        public string usua_password { get; set; }
    }
}