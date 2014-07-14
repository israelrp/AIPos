using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.IGeneric;
using AIPos.DataContext;
using AIPos.Domain;

namespace AIPos.DAO.Implementation
{
    public class ServicioApartadoDaoImpl : ServicioApartadoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public ServicioApartado Insert(ServicioApartado entity)
        {
            object[] parameters = new object[] { entity.VentaId, entity.DireccionEnvioId, entity.EstatusId, entity.FechaEntrega, entity.Especificaciones,
                entity.Tipo, entity.Anticipo };
            return cm.Database.SqlQuery<ServicioApartado>("dbo.usp_ServiciosApartadosInsert @VentaId={0}, @DireccionEnvioId={1}, @EstatusId={2}, "+
                "@FechaEntrega={3}, @Especificaciones={4}, @Tipo={5}, @Anticipo={6}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ServiciosApartadosDelete @VentaId={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public ServicioApartado Update(ServicioApartado entity)
        {
            object[] parameters = new object[] { entity.VentaId, entity.DireccionEnvioId, entity.EstatusId, entity.FechaEntrega, entity.Especificaciones,
                entity.Tipo, entity.Anticipo };
            return cm.Database.SqlQuery<ServicioApartado>("dbo.usp_ServiciosApartadosUpdate @VentaId={0}, @DireccionEnvioId={1}, @EstatusId={2}, " +
                "@FechaEntrega={3}, @Especificaciones={4}, @Tipo={5}, @Anticipo={6}", parameters).FirstOrDefault();
        }

        public List<ServicioApartado> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<ServicioApartado>("dbo.usp_ServiciosApartadosSelect @VentaId={0}", parameters).ToList();
        }

        public ServicioApartado SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<ServicioApartado>("dbo.usp_ServiciosApartadosSelect @VentaId={0}", parameters).FirstOrDefault();
        }

        public List<ReporteServicioApartado> RecuperarReporteServiciosApartados(int SucursalId, int? EstatusId)
        {
            object[] parameters = new object[] { SucursalId, EstatusId };
            return cm.Database.SqlQuery<ReporteServicioApartado>("dbo.usp_ReporteServiciosApartados @SucursalId={0}, @EstatusId={1}", parameters).ToList();
        }
    }
}
