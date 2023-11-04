using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using service_comisiones.Context;
using service_comisiones.Entities.DBGgrSion;

namespace service_comisiones.Repository
{
    public class AdministracionContactoRepository
    {
        private readonly ILogger<AdministracionContactoRepository> _logger;
        private readonly DbGrdSionContext _dbGrdSionContext;

        public AdministracionContactoRepository(
            ILogger<AdministracionContactoRepository> logger,
            DbGrdSionContext dbGrdSionContext
        )
        {
            this._logger = logger;
            this._dbGrdSionContext = dbGrdSionContext;
        }
        public async Task<AdministracionContacto> GetOne(long lcontacto_id)
        {
            this._logger.LogInformation($"administracionContactoRepository/GetOne({lcontacto_id}) inizializando...");
            var ultimo = await this._dbGrdSionContext.administracioncontacto.Where(x=>x).FirstOrDefaultAsync();
            if (ultimo == null)
            {
                this._logger.LogCritical($"administracionContactoRepository/GetUltimoCodigo => no se pudo obtener el ultimo codigo");
                throw new Exception("Error persona no registrada");
            }
            return ultimo;
        }
        public async Task<AdministracionContacto> Store(AdministracionContacto administracionContacto)
        {
            this._logger.LogInformation($"administracionContactoRepository/Store({JsonConvert.SerializeObject(administracionContacto, Formatting.Indented)}) inizializando...");
            await this._dbGrdSionContext.AddAsync(administracionContacto);
            var ejecucion = await _dbGrdSionContext.SaveChangesAsync();
            if (Convert.ToBoolean(ejecucion))
            {
                this._logger.LogInformation("administracionContactoRepository/Store => Insertado correctamente");
            }
            else
            {
                this._logger.LogCritical($"administracionContactoRepository/Store => no se puedo registrar");
                throw new Exception("Error persona no registrada");
            }
            return administracionContacto;
        }
        public async Task<AdministracionContacto> GetUltimateLcontacto()
        {
            this._logger.LogInformation($"administracionContactoRepository/GetUltimateLcontacto() inizializando...");
            var ultimo = await this._dbGrdSionContext.administracioncontacto.OrderByDescending(x => x.lcontacto_id).FirstOrDefaultAsync();
            if (ultimo == null)
            {
                this._logger.LogCritical($"administracionContactoRepository/GetUltimateLcontacto => no se pudo obtener el ultmo lcontactoId");
                throw new Exception("Error persona no registrada");
            }
            return ultimo;
        }
        public async Task<AdministracionContacto> GetUltimoCodigo()
        {
            this._logger.LogInformation($"administracionContactoRepository/GetUltimoCodigo() inizializando...");
            var ultimo = await this._dbGrdSionContext.administracioncontacto.OrderByDescending(x => x.scodigo).FirstOrDefaultAsync();
            if (ultimo == null)
            {
                this._logger.LogCritical($"administracionContactoRepository/GetUltimoCodigo => no se pudo obtener el ultimo codigo");
                throw new Exception("Error persona no registrada");
            }
            return ultimo;
        }
    }
}