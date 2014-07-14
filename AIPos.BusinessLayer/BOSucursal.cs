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
    public class BOSucursal
    {
        SucursalDaoImpl sucursalDaoImpl = new SucursalDaoImpl();

        public Sucursal Insert(Sucursal sucursal)
        {
            sucursal = sucursalDaoImpl.Insert(sucursal);
            return sucursal;
        }

        public void Delete(int id)
        {
            if (!sucursalDaoImpl.Delete(id))
            {
                throw new Exception("No fue posible eliminar la unidad");
            }
        }

        public Sucursal Update(Sucursal sucursal)
        {
            sucursal = sucursalDaoImpl.Update(sucursal);
            return sucursal;
        }

        public List<Sucursal> SelectAll()
        {
            return sucursalDaoImpl.SelectAll();
        }

        public Sucursal SelectById(int id)
        {
            return sucursalDaoImpl.SelectByKey(id);
        }
    }
}
