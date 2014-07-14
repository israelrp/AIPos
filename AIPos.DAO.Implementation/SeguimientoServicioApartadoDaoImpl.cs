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
    public class SeguimientoServicioApartadoDaoImpl : SeguimientoServicioApartadoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public SeguimientoServicioApartado Insert(SeguimientoServicioApartado entity)
        {
            object[] parameters = new object[] { entity.VentaId, entity.FechaSolicitud, entity.FechaSalidaRepartidor, entity.FechaLlegadaRepartidor, entity.Repartidor };
            return cm.Database.SqlQuery<SeguimientoServicioApartado>("dbo.usp_SeguimientosServiciosApartadosInsert @VentaId={0}, @FechaSolicitud={1}, @FechaSalidaRepartidor={2},"+
                "@FechaLlegadaRepartidor={3}, @Repartidor={4}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_SeguimientosServiciosApartadosDelete @VentaId={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public SeguimientoServicioApartado Update(SeguimientoServicioApartado entity)
        {
            object[] parameters = new object[] { entity.VentaId, entity.FechaSolicitud, entity.FechaSalidaRepartidor, entity.FechaLlegadaRepartidor, entity.Repartidor };
            return cm.Database.SqlQuery<SeguimientoServicioApartado>("dbo.usp_SeguimientosServiciosApartadosUpdate @VentaId={0}, @FechaSolicitud={1}, @FechaSalidaRepartidor={2}," +
                "@FechaLlegadaRepartidor={3}, @Repartidor={4}", parameters).FirstOrDefault();
        }

        public List<SeguimientoServicioApartado> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<SeguimientoServicioApartado>("dbo.usp_SeguimientosServiciosApartadosSelect @VentaId={0}", parameters).ToList();
        }

        public SeguimientoServicioApartado SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<SeguimientoServicioApartado>("dbo.usp_SeguimientosServiciosApartadosSelect @VentaId={0}", parameters).FirstOrDefault();
        }
    }
}
