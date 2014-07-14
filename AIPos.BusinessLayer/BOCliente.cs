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
    public class BOCliente
    {
        ClienteDaoImpl clienteDaoImpl = new ClienteDaoImpl();

        public Cliente Insert(Cliente cliente)
        {
            cliente = clienteDaoImpl.Insert(cliente);
            return cliente;
        }

        public void Delete(int id)
        {
            List<Direccion> direcciones = new BODireccion().SelectByClienteId(id);
            if (direcciones != null)
            {
                foreach (var direccion in direcciones)
                {
                    BODireccion boDireccion = new BODireccion();
                    BODireccionEnvio boDireccionEnvio = new BODireccionEnvio();
                    boDireccionEnvio.Delete(direccion.Id, id);
                    boDireccion.Delete(direccion.Id);
                }
            }
            ClienteListaPrecio clienteListaPrecio = new BOClienteListaPrecio().SelectByCliente(id);
            if (clienteListaPrecio != null)
            {
                BOClienteListaPrecio boClienteListaPrecio = new BOClienteListaPrecio();
                boClienteListaPrecio.Delete(clienteListaPrecio);
            }
            if (!clienteDaoImpl.Delete(id))
            {
                throw new Exception("No fue posible eliminar el cliente");
            }
        }

        public Cliente Update(Cliente cliente)
        {
            cliente = clienteDaoImpl.Update(cliente);
            return cliente;
        }

        public List<Cliente> SelectAll()
        {
            return clienteDaoImpl.SelectAll();
        }

        public Cliente SelectById(int id)
        {
            return clienteDaoImpl.SelectByKey(id);
        }

        public Cliente SelectByCodigo(string Codigo)
        {
            return clienteDaoImpl.SelectAll().Where(x => x.Codigo.ToLower() == Codigo.ToLower()).FirstOrDefault();
        }
    }
}
