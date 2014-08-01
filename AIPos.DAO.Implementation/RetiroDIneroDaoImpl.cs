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
    public class RetiroDIneroDaoImpl : RetiroDineroDao
    {
        ConnectionManager cm = new ConnectionManager();

        public RetiroDinero Insert(RetiroDinero entity)
        {
            object[] parameters = new object[] { entity.UsuarioId, entity.SucursalId, entity.Monto, entity.Descripcion, entity.Fecha, entity.EsCorteCaja };
            return cm.Database.SqlQuery<RetiroDinero>("dbo.usp_RetirosDineroInsert @UsuarioId={0}, @SucursalId={1}, @Monto={2}, @Descripcion={3}, @Fecha={4}, "+
                "@EsCorteCaja={5}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_RetirosDineroDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public RetiroDinero Update(RetiroDinero entity)
        {
            object[] parameters = new object[] { entity.Id, entity.UsuarioId, entity.SucursalId, entity.Monto, entity.Descripcion, entity.Fecha, entity.EsCorteCaja };
            return cm.Database.SqlQuery<RetiroDinero>("dbo.usp_RetirosDinerosUpdate @Id={0}, @UsuarioId={1}, @SucursalId={2}, @Monto={3}, @Descripcion={4}, @Fecha={5}, "+
                "@EsCorteCaja={6}", parameters).FirstOrDefault();
        }

        public List<RetiroDinero> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<RetiroDinero>("dbo.usp_RetirosDineroSelect @Id={0}", parameters).ToList();
        }

        public RetiroDinero SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<RetiroDinero>("dbo.usp_RetirosDineroSelect @Id={0}", parameters).FirstOrDefault();
        }

        public List<RetiroDinero> SelectAllByFechaSucursal(DateTime FechaInicio, DateTime FechaFin, int SucursalId)
        {
            object[] parameters = new object[] { FechaInicio, FechaFin, SucursalId };
            return cm.Database.SqlQuery<RetiroDinero>("dbo.usp_RetirosDineroSelectByFechaSucursal @FechaInicio={0}, @FechaFin={1}, @SucursalId={2}", parameters).ToList();
        }

        public List<ReporteRetiroDinero> SelectReporteByFechaSucursal(DateTime FechaInicio, DateTime FechaFin, int SucursalId, bool EsCorteCaja)
        {
            object[] parameters = new object[] { FechaInicio, FechaFin, SucursalId, EsCorteCaja };
            return cm.Database.SqlQuery<ReporteRetiroDinero>("dbo.usp_RetirosDineroReporteSelectByFechaSucursal @FechaInicio={0}, @FechaFin={1}, @SucursalId={2}, @EsCorteCaja={3}", parameters).ToList();
        }
    }
}
