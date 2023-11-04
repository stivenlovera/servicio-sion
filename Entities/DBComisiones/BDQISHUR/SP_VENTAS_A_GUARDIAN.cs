using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace service_comisiones.Entities.DBComisiones.BDQISHUR
{
    [Keyless]
    public class SP_VENTAS_A_GUARDIAN
    {
        public string EMPRESA { get; set; }
        public decimal IDALMACEN { get; set; }
        public decimal IDVENTA { get; set; }
        public DateTime FECHAVENTA { get; set; }
        public string IDPRODUCTO { get; set; }
        public decimal IDCLIENTE { get; set; }
        public string CI_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public decimal IDVENDEDOR { get; set; }
        public string CI_VENDEDOR { get; set; }
        public string NOMBRE_VENDEDOR { get; set; }
        public decimal IDTIPOVENTA { get; set; }
        public decimal PRECIOVENTA { get; set; }
        public decimal CUOTAINICIAL { get; set; }
        public decimal VALOR_CI { get; set; }
        public decimal MONTOABONADO { get; set; }
        public decimal TOTALDEUDA { get; set; }
        public string TIPOVENTA { get; set; }
        public int IDESTADO_VENTA { get; set; }
        public decimal IDESTADO { get; set; }
        public string GLOSA { get; set; }
        public string NRODOC { get; set; }
        public int? KIT { get; set; }
        public int COMISIONABLE { get; set; }
        public DateTime MODFECHA { get; set; }
        public string MODHORA { get; set; }
    }
}