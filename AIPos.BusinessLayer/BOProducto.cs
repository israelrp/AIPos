using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO;
using AIPos.DAO.Implementation;
using AIPos.Domain;

namespace AIPos.BusinessLayer
{
    public class BOProducto
    {
        ProductoDaoImpl productoDaoImpl = new ProductoDaoImpl();

        public Producto Insert(Producto producto)
        {
            producto = productoDaoImpl.Insert(producto);
            return producto;
        }

        public void Delete(int id)
        {
            BOListaPrecioProducto boListaPrecioProducto = new BOListaPrecioProducto();
            List<ListaPrecioProducto> listasPrecioProducto = boListaPrecioProducto.SelectByProducto(id);
            foreach (var lista in listasPrecioProducto)
            {
                boListaPrecioProducto.Delete(lista);
            }
            if (!productoDaoImpl.Delete(id))
            {
                throw new Exception("No fue posible eliminar la unidad");
            }
        }

        public Producto Update(Producto producto)
        {
            producto = productoDaoImpl.Update(producto);
            return producto;
        }

        public List<Producto> SelectAll()
        {
            return productoDaoImpl.SelectAll();
        }

        public Producto SelectById(int id)
        {
            return productoDaoImpl.SelectByKey(id);
        }

        public Producto SelectByCodigo(string Codigo)
        {
            Producto producto = productoDaoImpl.SelectAll().Where(x => x.Codigo.ToLower() == Codigo.ToLower()).FirstOrDefault();
            return producto;
        }


    }
}
