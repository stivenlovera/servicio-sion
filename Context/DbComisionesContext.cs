using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using service_comisiones.Entities.DBComisiones;
using service_comisiones.Entities.DBComisiones.BDQISHUR;

namespace service_comisiones.Context
{
    public class DbComisionesContext : DbContext
    {
        public DbComisionesContext(DbContextOptions<DbComisionesContext> options) : base(options)
        {

        }
        //Resultados de procedimiento almacenado
        public virtual DbSet<SP_VENTAS_A_GUARDIAN> SP_VENTAS_A_GUARDIAN { get; set; }
        public virtual DbSet<ObtenerCliente> obtenerclientes { get; set; }
    }
}