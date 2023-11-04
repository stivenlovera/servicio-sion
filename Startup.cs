using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using service_comisiones.Context;
using service_comisiones.Modules;
using service_comisiones.Querys;
using service_comisiones.Repository;
using service_comisiones.Utils;
using ServiceComisiones.PlantillaHtml;

namespace service_comisiones
{
    public class Startup
    {
        private readonly IHostBuilder _hostBuilder;

        public Startup(IHostBuilder hostBuilder)
        {
            this._hostBuilder = hostBuilder;
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                this.ServicesConexionDataBase(services, hostContext);
                this.Services(services);
                this.ServicesModules(services);
                this.ServicesUtils(services);
                this.ServicesQuerys(services);
                this.ServicesRepository(services);
                this.ServicesMessagesHtml(services);
            });
        }
        public void ServicesConexionDataBase(IServiceCollection services, HostBuilderContext hostContext)
        {
            /*Conexion mysql*/
            var getStringConnectionMysql = hostContext.Configuration.GetSection("connectionMysql").Get<StringConnection>();
            var mysqlConnect = $"Server={getStringConnectionMysql.IpServer};Port={getStringConnectionMysql.Port};Database={getStringConnectionMysql.Database};User={getStringConnectionMysql.User};Password={getStringConnectionMysql.Password};Connection Timeout=30;default command timeout=0;";
            var optionsBuilder = new DbContextOptionsBuilder<DbGrdSionContext>();
            optionsBuilder.UseMySql(mysqlConnect, ServerVersion.AutoDetect(mysqlConnect)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); ;
            services.AddScoped<DbGrdSionContext>(db => new DbGrdSionContext(optionsBuilder.Options));

            /*Conexion sqlserver DBComisiones*/
            var getStringConnectionSqlServerDBComisiones = hostContext.Configuration.GetSection("connectionSqlDBComisiones").Get<StringConnection>();
            var sqlConnectDBComisiones = $"Server={getStringConnectionSqlServerDBComisiones.IpServer};Database={getStringConnectionSqlServerDBComisiones.Database};User={getStringConnectionSqlServerDBComisiones.User};Password={getStringConnectionSqlServerDBComisiones.Password};TrustServerCertificate=True;";
            var optionsBuilderDBComisiones = new DbContextOptionsBuilder<DbComisionesContext>();
            optionsBuilderDBComisiones.UseSqlServer(sqlConnectDBComisiones).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            services.AddScoped<DbComisionesContext>(db => new DbComisionesContext(optionsBuilderDBComisiones.Options));
        }

        public void Services(IServiceCollection services)
        {
            //services.AddHostedService<Worker>();
            services.AddHostedService<VentaService>();
        }
        /*Modulos*/
        public void ServicesModules(IServiceCollection services)
        {
            services.AddTransient<VentasModule>();
        }
        /*Utilidades*/
        public void ServicesUtils(IServiceCollection services)
        {
            services.AddTransient<HttpClientService>();
        }
        /*Repository*/
        public void ServicesRepository(IServiceCollection services)
        {
            services.AddTransient<AdministracionContactoRepository>();
        }
        /*Querys*/
        public void ServicesQuerys(IServiceCollection services)
        {
            services.AddTransient<VentasQuery>();
        }
        /*Messages email*/
        public void ServicesMessagesHtml(IServiceCollection services)
        {
            services.AddTransient<HtmlAlertaMessage>();
        }
    }
}