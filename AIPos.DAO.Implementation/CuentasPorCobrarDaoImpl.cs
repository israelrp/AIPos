using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DataContext;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO.Implementation
{
    public class CuentasPorCobrarDaoImpl :CuentaPorCobrarDao
    {
        ConnectionManager cm = new ConnectionManager();

        public CuentaPorCobrar Insert(CuentaPorCobrar entity)
        {
            object[] parameters = new object[] { entity.ClienteId, entity.UsuarioId, entity.VentaId, entity.Importe, entity.Descripcion, entity.Fecha, entity.FechaLimite, entity.Estatus };
            return cm.Database.SqlQuery<CuentaPorCobrar>("dbo.usp_CuentasPorCobrarInsert @ClienteId={0}, @UsuarioId={1}, @VentaId={2}, @Importe={3}, @Descripcion={4}, @Fecha={5}, @FechaLimite={6}, @Estatus={7}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_CuentasPorCobrarDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public CuentaPorCobrar Update(CuentaPorCobrar entity)
        {
            object[] parameters = new object[] { entity.Id, entity.ClienteId, entity.UsuarioId, entity.VentaId, entity.Importe, entity.Descripcion, entity.Fecha, entity.FechaLimite, entity.Estatus };
            return cm.Database.SqlQuery<CuentaPorCobrar>("dbo.usp_CuentasPorCobrarUpdate @Id={0}, @ClienteId={1}, @UsuarioId={2}, @VentaId={3}, @Importe={4}, @Descripcion={5}, @Fecha={6}, @FechaLimite={7}, @Estatus={8}", parameters).FirstOrDefault();
        }

        public List<CuentaPorCobrar> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<CuentaPorCobrar>("dbo.usp_CuentasPorCobrarSelect @Id={0}", parameters).ToList();
        }

        public CuentaPorCobrar SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<CuentaPorCobrar>("dbo.usp_CuentasPorCobrarSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
