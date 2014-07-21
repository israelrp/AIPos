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
    public class BOListaPrecioProducto
    {
        ListaPrecioProductoDaoImpl listaPrecioProductoDaoImpl = new ListaPrecioProductoDaoImpl();

        public ListaPrecioProducto Insert(ListaPrecioProducto listaPrecioProducto)
        {
            return listaPrecioProductoDaoImpl.Insert(listaPrecioProducto);
        }

        public void Delete(ListaPrecioProducto entity)
        {
            listaPrecioProductoDaoImpl.Delete(entity);
        }

        public ListaPrecioProducto Update(ListaPrecioProducto listaPrecioProducto)
        {
            return listaPrecioProductoDaoImpl.Update(listaPrecioProducto);
        }

        public ListaPrecioProducto SelectById(ListaPrecioProducto listaPrecioProducto)
        {
            return listaPrecioProductoDaoImpl.SelectByKey(listaPrecioProducto);
        }

        public ListaPrecioProducto SelectByProductoListaPrecio(int ListaPrecioId, int ProductoId)
        {
            return listaPrecioProductoDaoImpl.SelectByKey(new ListaPrecioProducto() { ListaPrecioId = ListaPrecioId, ProductoId = ProductoId });
        }

        public ListaPrecioProducto SelectByProductoSucursal(int SucursalId, int ProductoId)
        {
            BOSucursalListaPrecio boSucursalListaPrecio = new BOSucursalListaPrecio();
            SucursalListaPrecio sucursalListaPrecio=boSucursalListaPrecio.SelectBySucursal(SucursalId);
            if (sucursalListaPrecio == null)
            {
                return null;
            }
            return listaPrecioProductoDaoImpl.SelectByKey(new ListaPrecioProducto() { ListaPrecioId = sucursalListaPrecio.ListaPrecioId, ProductoId = ProductoId });
        }

        public ListaPrecioProducto SelectByProductoLista(int ListaId, int ProductoId)
        {
            BOListaPrecio boListaPrecio = new BOListaPrecio();
            ListaPrecio listaPrecio = boListaPrecio.SelectById(ListaId);
            if (listaPrecio == null)
            {
                return null;
            }
            return listaPrecioProductoDaoImpl.SelectByKey(new ListaPrecioProducto() { ListaPrecioId = listaPrecio.Id, ProductoId = ProductoId });
        }

        public List<ListaPrecioProducto> SelectByProducto(int ProductoId)
        {
            return listaPrecioProductoDaoImpl.SelectByProducto(ProductoId);
        }


        public ListaPrecioProducto SelectByProductoCliente(int ClienteId, int ProductoId)
        {
            BOClienteListaPrecio boClienteListaPrecio = new BOClienteListaPrecio();
            ClienteListaPrecio clienteListaPrecio = boClienteListaPrecio.SelectByCliente(ClienteId);
            if (clienteListaPrecio == null)
            {
                return null;
            }
            return listaPrecioProductoDaoImpl.SelectByKey(new ListaPrecioProducto() { ListaPrecioId = clienteListaPrecio.ListaPrecioId, ProductoId = ProductoId });
        }

        public List<ListaPrecioProductoModel> SelectByListaPrecio(int ListaPrecioId, string Codigo, string Nombre)
        {
            List<ListaPrecioProductoModel> datos = listaPrecioProductoDaoImpl.SelectByListaPrecio(ListaPrecioId);
            if (Codigo != "")
            {
                datos = datos.Where(x => x.Codigo.ToLower() == Codigo.ToLower()).ToList();
            }
            if (Nombre != "")
            {
                datos = datos.Where(x => x.Producto.ToLower() == Nombre.ToLower()).ToList();
            }
            return datos;
        }
        
    }
}
