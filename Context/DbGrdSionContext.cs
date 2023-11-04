using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using service_comisiones.Entities.DBComisiones;
using service_comisiones.Entities.DBGgrSion;

namespace service_comisiones.Context
{
    public class DbGrdSionContext : DbContext
    {
        public DbGrdSionContext(DbContextOptions<DbGrdSionContext> options) : base(options)
        {

        }
        //Tablas
        public virtual DbSet<Temporal_Ventas> temporal_tentas { get; set; }
        public virtual DbSet<AdministracionContacto> administracioncontacto { get; set; }

    }
}