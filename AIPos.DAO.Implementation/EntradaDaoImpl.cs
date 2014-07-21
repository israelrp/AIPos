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
    public class EntradaDaoImpl : EntradaDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Entrada Insert(Entrada entity)
        {
            object[] parameters = new object[] { entity.ProveedorId, entity.ProductoId, entity.UsuarioId, entity.SucursalId,
                entity.TipoProductoId, entity.CantidadReal, entity.TotalPiezas, entity.Fecha, entity.CantidadProveedor,
                entity.Importe, entity.PrecioUnitario, entity.Diferencia };
            return cm.Database.SqlQuery<Entrada>("dbo.usp_EntradasInsert @ProveedorId={0}, @ProductoId={1}, @UsuarioId={2}, @SucursalId={3}, "+
                "@TipoProductoId={4}, @CantidadReal={5}, @TotalPiezas={6}, @Fecha={7}, @CantidadProveedor={8}, @Importe={9}, @PrecioUnitario={10}, "+
                "@Diferencia={11}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_EntradasDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Entrada Update(Entrada entity)
        {
            object[] parameters = new object[] { entity.Id, entity.ProveedorId, entity.ProductoId, entity.UsuarioId, entity.SucursalId,
                entity.TipoProductoId, entity.CantidadReal, entity.TotalPiezas, entity.Fecha, entity.CantidadProveedor,
                entity.Importe, entity.PrecioUnitario, entity.Diferencia };
            return cm.Database.SqlQuery<Entrada>("dbo.usp_EntradasUpdate @Id={0}, @ProveedorId={1}, @ProductoId={2}, @UsuarioId={3}, @SucursalId={4}, " +
                "@TipoProductoId={5}, @CantidadReal={6}, @TotalPiezas={7}, @Fecha={8}, @CantidadProveedor={9}, @Importe={10}, @PrecioUnitario={11}, " +
                "@Diferencia={12}", parameters).FirstOrDefault();
        }

        public List<Entrada> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Entrada>("dbo.usp_EntradasSelect @Id={0}", parameters).ToList();
        }

        public List<Entrada> SelectByFechaSucursal(DateTime FechaInicio, DateTime FechaFin, int SucursalId)
        {
            object[] parameters = new object[] { FechaInicio,FechaFin, SucursalId };
            return cm.Database.SqlQuery<Entrada>("dbo.usp_EntradasSelectByFechaSucursal @FechaInicio={0}, @FechaFin={1}, @SucursalId={2}", parameters).ToList();
        }


        public Entrada SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Entrada>("dbo.usp_EntradasSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
