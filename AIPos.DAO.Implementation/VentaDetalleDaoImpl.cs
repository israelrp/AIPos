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
    public class VentaDetalleDaoImpl : VentaDetalleDao
    {
        ConnectionManager cm = new ConnectionManager();

        public VentaDetalle Insert(VentaDetalle entity)
        {
            object[] parameters = new object[] { entity.VentaId, entity.ProductoId, entity.Cantidad, entity.PrecioUnitario, entity.Codigo, entity.Nombre,
                entity.Fecha, entity.Descuento, entity.Importe };
            return cm.Database.SqlQuery<VentaDetalle>("dbo.usp_VentasDetalleInsert @VentaId={0}, @ProductoId={1}, @Cantidad={2}, @PrecioUnitario={3}, @Codigo={4}, " +
                "@Nombre={5}, @Fecha={6}, @Descuento={7}, @Importe={8}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_VentasDetalleDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public VentaDetalle Update(VentaDetalle entity)
        {
            object[] parameters = new object[] { entity.Id, entity.VentaId, entity.ProductoId, entity.Cantidad, entity.PrecioUnitario, entity.Codigo, entity.Nombre,
                entity.Fecha, entity.Fecha, entity.Descuento, entity.Importe };
            return cm.Database.SqlQuery<VentaDetalle>("dbo.usp_VentasDetalleUpdate @Id={0}, @VentaId={1}, @ProductoId={2}, @Cantidad={3}, @PrecioUnitario={4}, @Codigo={5}, " +
                "@Nombre={6}, @Fecha={7}, @Descuento={8}, @Importe={9}", parameters).FirstOrDefault();
        }

        public List<VentaDetalle> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<VentaDetalle>("dbo.usp_VentasDetalleSelect @Id={0}", parameters).ToList();
        }

        public VentaDetalle SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<VentaDetalle>("dbo.usp_VentasDetalleSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
