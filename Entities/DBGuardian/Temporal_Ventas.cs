using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace service_comisiones.Entities.DBGgrSion
{
    public class Temporal_Ventas
    {
        public string EMPRESA { get; set; }
        public int IDALMACEN { get; set; }
        [Key]
        public int IDVENTA { get; set; }
        public DateTime FECHAVENTA { get; set; }
        public int IDCLIENTE { get; set; }
        public string CI_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public int IDVENDEDOR { get; set; }
        public string CI_VENDEDOR { get; set; }
        public string NOMBRE_VENDEDOR { get; set; }
        public int IDTIPOVENTA { get; set; }
        public int PRECIOVENTA { get; set; }
        public int CUOTAINICIAL { get; set; }
        public int VALOR_CI { get; set; }
        public int MONTOABONADO { get; set; }
        public int TOTALDEUDA { get; set; }
        public string TIPOVENTA { get; set; }
        public int IDESTADO_VENTA { get; set; }
        public int IDESTADO { get; set; }
        public string GLOSA { get; set; }
        public string NRODOC { get; set; }
        public int KIT { get; set; }
        public int COMISIONABLE { get; set; }
        public DateTime MODFECHA { get; set; }
        public string MODHORA { get; set; }
        public string ESTADOVENTA { get; set; }
    }
}