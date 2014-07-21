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
    public class BOClienteListaPrecio
    {
        ClienteListaPrecioDaoImpl clienteListaPrecioDaoImpl = new ClienteListaPrecioDaoImpl();

        public ClienteListaPrecio Insert(ClienteListaPrecio clienteListaPrecio)
        {
            clienteListaPrecio = clienteListaPrecioDaoImpl.Insert(clienteListaPrecio);
            return clienteListaPrecio;
        }

        public void Delete(ClienteListaPrecio clienteListaPrecio)
        {
            if (!clienteListaPrecioDaoImpl.Delete(clienteListaPrecio))
            {
                throw new Exception("No fue posible eliminar la lista de precio al cliente");
            }
        }

        public ClienteListaPrecio CambiarListaPrecioACliente(ClienteListaPrecio clienteListaPrecio)
        {
            clienteListaPrecio = clienteListaPrecioDaoImpl.Update(clienteListaPrecio);
            return clienteListaPrecio;
        }

        public List<ClienteListaPrecio> SelectByListaPrecio(int ListaPrecioId)
        {
            return clienteListaPrecioDaoImpl.SelectByKey(null, ListaPrecioId);
        }

        public ClienteListaPrecio SelectByCliente(int ClienteId)
        {
            return clienteListaPrecioDaoImpl.SelectByKey(ClienteId, null);
        }

        public List<ClienteListaPrecioModel> SelectAllGrid()
        {
            return clienteListaPrecioDaoImpl.SelectAllGrid();
        }


    }
}
