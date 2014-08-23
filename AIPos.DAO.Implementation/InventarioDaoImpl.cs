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
    public class InventarioDaoImpl : InventarioDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Inventario Insert(Inventario entity)
        {
            object[] parameters = new object[] { entity.UsuarioId, entity.ProductoId, entity.SucursalId, entity.CantidadReal, entity.CantidadSistema,
                entity.FechaRegistro, entity.PrecioUnitario, entity.Monto };
            return cm.Database.SqlQuery<Inventario>("dbo.usp_InventariosInsert @UsuarioId={0}, @ProductoId={1}, @SucursalId={2}, @CantidadReal={3}, "+
                "@CantidadSistema={4}, @FechaRegistro={5}, @PrecioUnitario={6}, @Monto={7}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_InventariosDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Inventario Update(Inventario entity)
        {
            object[] parameters = new object[] { entity.Id,  entity.UsuarioId, entity.ProductoId, entity.SucursalId, entity.CantidadReal, entity.CantidadSistema,
                entity.FechaRegistro, entity.PrecioUnitario, entity.Monto };
            return cm.Database.SqlQuery<Inventario>("dbo.usp_InventariosUpdate @Id={0}, @UsuarioId={1}, @ProductoId={2}, @SucursalId={3}, @CantidadReal={4}, " +
                "@CantidadSistema={5}, @FechaRegistro={6}, @PrecioUnitario={7}, @Monto={8}", parameters).FirstOrDefault();
        }

        public List<Inventario> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Inventario>("dbo.usp_InventariosSelect @Id={0}", parameters).ToList();
        }

        public Inventario SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Inventario>("dbo.usp_InventariosSelect @Id={0}", parameters).FirstOrDefault();
        }


        public List<Inventario> SelectByFechaSucursal(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<Inventario>("dbo.usp_InventariosSelectByFechaSucursal @SucursalId={0}, @FechaInicio={1}, @FechaFin={2}", parameters).ToList();
        }

        public List<ReporteInventario> SelectReporteFechaSucursal(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<ReporteInventario>("dbo.usp_InventariosReporteByFechaSucursal @SucursalId={0}, @FechaInicio={1}, @FechaFin={2}", parameters).ToList();
        }

        public List<ReporteInventarioUtilidad> SelectReporteUtilidadFechaSucursal(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<ReporteInventarioUtilidad>("dbo.usp_InventariosUtilidadReporteByFechaSucursal @SucursalId={0}, @FechaInicio={1}, @FechaFin={2}", parameters).ToList();
        }
    }
}
