using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace service_comisiones.Entities.DBGgrSion
{
    public class AdministracionContacto
    {
        [Key]
        public string susuarioadd { get; set; }
        public DateTime dtfechaadd { get; set; }
        public string susuariomod { get; set; }
        public DateTime dtfechamod { get; set; }
        public long lcontacto_id { get; set; }
        public string scodigo { get; set; }
        public string stelefonofijo { get; set; }
        public string stelefonomovil { get; set; }
        public string scorreoelectronico { get; set; }
        public string cestado { get; set; }
        public byte[] bifoto { get; set; }
        public DateTime dtfechanacimiento { get; set; }
        public string sdireccion { get; set; }
        public long lpais_id { get; set; }
        public string sciudad { get; set; }
        public string scedulaidentidad { get; set; }
        public long lpatrocinante_id { get; set; }
        public long lnivel_id { get; set; }
        public DateTime dtfecharegistro { get; set; }
        public string snombrecompleto { get; set; }
        public DateTime dtfechacalculo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal dlotes { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal dproduccion { get; set; }
        public string cvolante { get; set; }
        public string cpresentacion { get; set; }
        public string ccena { get; set; }
        public string ctv { get; set; }
        public string cperiodico { get; set; }
        public string cradio { get; set; }
        public string cweb { get; set; }
        public string sotro { get; set; }
        public string ccorreo { get; set; }
        public long ltipocontacto_id { get; set; }
        public string cpresentafactura { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ddescuentolote { get; set; }
        public string snotadescuentolote { get; set; }
        public string stelefonooficina { get; set; }
        public string scontrasena { get; set; }
        public long lpatrotemp_id { get; set; }
        public long lnit { get; set; }
        public string lcuentabanco { get; set; }
        public long lcodigobanco { get; set; }
        public string ctienecuenta { get; set; }
        public string cbaja { get; set; }
        public DateTime dtfechabaja { get; set; }
        public int ltipobaja { get; set; }
        public string smotivobaja { get; set; }
        public long pmax { get; set; }
        public long pvitalicio { get; set; }
    }
}