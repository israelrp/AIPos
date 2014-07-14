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
    public class BODireccion
    {
        DireccionDaoImpl direccionDaoImpl = new DireccionDaoImpl();

        public Direccion Insert(Direccion direccion)
        {
            direccion = direccionDaoImpl.Insert(direccion);
            return direccion;
        }

        public void Delete(int id)
        {
            if (!direccionDaoImpl.Delete(id))
            {
                throw new Exception("No fue posible eliminar la unidad");
            }
        }

        public Direccion Update(Direccion direccion)
        {
            direccion = direccionDaoImpl.Update(direccion);
            return direccion;
        }

        public List<Direccion> SelectAll()
        {
            return direccionDaoImpl.SelectAll();
        }

        public Direccion SelectById(int id)
        {
            return direccionDaoImpl.SelectByKey(id);
        }

        public List<Direccion> SelectByClienteId(int ClienteId)
        {
            return direccionDaoImpl.SelectByCliente(ClienteId);
        }
    }
}
