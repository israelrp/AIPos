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
    public class ListaPrecioProductoDaoImpl : ListaPrecioProductoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public ListaPrecioProducto Insert(ListaPrecioProducto entity)
        {
            object[] parameters = new object[] { entity.ListaPrecioId, entity.ProductoId, entity.Precio, entity.Descuento };
            return cm.Database.SqlQuery<ListaPrecioProducto>("dbo.usp_ListasPrecioProductosInsert @ListaPrecioId={0}, @ProductoId={1}, @Precio={2}, @Descuento={3}", parameters).FirstOrDefault();
        }

        public bool Delete(ListaPrecioProducto key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key.ListaPrecioId, key.ProductoId };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ListasPrecioProductosDelete @ListaPrecioId={0}, @ProductoId={1}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public ListaPrecioProducto Update(ListaPrecioProducto entity)
        {
            object[] parameters = new object[] { entity.ListaPrecioId, entity.ProductoId, entity.Precio, entity.Descuento };
            return cm.Database.SqlQuery<ListaPrecioProducto>("dbo.usp_ListasPrecioProductosUpdate @ListaPrecioId={0}, @ProductoId={1}, @Precio={2}, @Descuento={3}", parameters).FirstOrDefault();
        }

        public List<ListaPrecioProducto> SelectAll()
        {
            object[] parameters = new object[] { null, null };
            return cm.Database.SqlQuery<ListaPrecioProducto>("dbo.usp_ListasPrecioProductosSelect @ListaPrecioId={0}, @ProductoId={1}", parameters).ToList();
        }

        public ListaPrecioProducto SelectByKey(ListaPrecioProducto key)
        {
            object[] parameters = new object[] { key.ListaPrecioId, key.ProductoId };
            return cm.Database.SqlQuery<ListaPrecioProducto>("dbo.usp_ListasPrecioProductosSelect @ListaPrecioId={0}, @ProductoId={1}", parameters).FirstOrDefault();
        }

        public List<ListaPrecioProducto> SelectByProducto(int ProductoId)
        {
            object[] parameters = new object[] { null, ProductoId };
            return cm.Database.SqlQuery<ListaPrecioProducto>("dbo.usp_ListasPrecioProductosSelect @ListaPrecioId={0}, @ProductoId={1}", parameters).ToList();
        }

        public List<ListaPrecioProductoModel> SelectByListaPrecio(int ListaPrecioId)
        {
            object[] parameters = new object[] { ListaPrecioId };
            return cm.Database.SqlQuery<ListaPrecioProductoModel>("dbo.usp_ListasPrecioProductosSelectGrid @ListaPrecioId={0}", parameters).ToList();
        }
    }
}
