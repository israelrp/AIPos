using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.BusinessLayer;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ISListaPrecioProducto" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ISListaPrecioProducto.svc or ISListaPrecioProducto.svc.cs at the Solution Explorer and start debugging.
    public class ISListaPrecioProducto : IISListaPrecioProducto
    {


        public ListaPrecioProducto SelectByProductoSucursal(int SucursalId, int ProductoId)
        {
            return new BOListaPrecioProducto().SelectByProductoSucursal(SucursalId, ProductoId);
        }

        public ListaPrecioProducto SelectByProductoCliente(int ClienteId, int ProductoId)
        {
            return new BOListaPrecioProducto().SelectByProductoCliente(ClienteId, ProductoId);
        }



        public ListaPrecioProducto SelectByProductoLista(int ListaId, int ProductoId)
        {
            return new BOListaPrecioProducto().SelectByProductoLista(ListaId, ProductoId);
        }
    }
}
