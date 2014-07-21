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
    public class BOListaPrecio
    {
        ListaPrecioDaoImpl listaPrecioDaoImpl = new ListaPrecioDaoImpl();

        public ListaPrecio Insert(ListaPrecio listaPrecio)
        {
            listaPrecio = listaPrecioDaoImpl.Insert(listaPrecio);
            List<Producto> productos = new ProductoDaoImpl().SelectAll();
            foreach (var producto in productos)
            {
                ListaPrecioProducto listaPrecioProducto = new ListaPrecioProducto();
                listaPrecioProducto.Descuento = 0;
                listaPrecioProducto.ListaPrecioId = listaPrecio.Id;
                listaPrecioProducto.Precio = 0;
                listaPrecioProducto.ProductoId = producto.Id;
                ListaPrecioProductoDaoImpl listaPrecioProductoDaoImpl = new ListaPrecioProductoDaoImpl();
                listaPrecioProductoDaoImpl.Insert(listaPrecioProducto);
            }
            return listaPrecio;
        }

        public void Delete(int Id)
        {
            List<ListaPrecioProducto> listaPreciosProductos = new ListaPrecioProductoDaoImpl().SelectAll().Where(x => x.ListaPrecioId == Id).ToList();
            if (listaPreciosProductos != null)
            {
                foreach (var listaPrecioProducto in listaPreciosProductos)
                {
                    ListaPrecioProductoDaoImpl listaPrecioProductoDaoImpl = new ListaPrecioProductoDaoImpl();
                    listaPrecioProductoDaoImpl.Delete(listaPrecioProducto);
                }
            }
            if (!listaPrecioDaoImpl.Delete(Id))
            {
                throw new Exception("No fue posible eliminar la Lista de precio");
            }
        }

        public ListaPrecio Update(ListaPrecio listaPrecio)
        {
            listaPrecio = listaPrecioDaoImpl.Update(listaPrecio);
            return listaPrecio;
        }

        public List<ListaPrecio> SelectAll()
        {
            return listaPrecioDaoImpl.SelectAll();
        }

        public ListaPrecio SelectById(int id)
        {
            return listaPrecioDaoImpl.SelectByKey(id);
        }

        public ListaPrecio RecuperarListaPrecioDelCliente(int ClienteId)
        {
            ListaPrecio listaPrecio;
            ClienteListaPrecio clienteListaPrecio = new ClienteListaPrecioDaoImpl().SelectByKey(ClienteId, null);
            if (clienteListaPrecio != null)
            {
                listaPrecio = listaPrecioDaoImpl.SelectByKey(clienteListaPrecio.ListaPrecioId);
                return listaPrecio;
            }
            else
            {
                throw new Exception("El cliente no tiene asignada una lista de precios");
            }
        }

        public ListaPrecio RecuperarListaPrecioDeSucursal(int SucursalId)
        {
            ListaPrecio listaPrecio;
            SucursalListaPrecio sucursalListaPrecio = new SucursalListaPrecioDaoImpl().SelectByKey(SucursalId, null);
            if (sucursalListaPrecio != null)
            {
                listaPrecio = listaPrecioDaoImpl.SelectByKey(sucursalListaPrecio.ListaPrecioId);
                return listaPrecio;
            }
            else
            {
                throw new Exception("La sucursal no tiene asignada una lista de precios");
            }
        }
    }
}
