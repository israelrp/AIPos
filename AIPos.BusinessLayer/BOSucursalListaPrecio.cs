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
    public class BOSucursalListaPrecio
    {
        SucursalListaPrecioDaoImpl sucursalListaPrecioDaoImpl = new SucursalListaPrecioDaoImpl();

        public SucursalListaPrecio Insert(SucursalListaPrecio sucursalListaPrecio)
        {
            sucursalListaPrecio = sucursalListaPrecioDaoImpl.Insert(sucursalListaPrecio);
            return sucursalListaPrecio;
        }

        public void Delete(SucursalListaPrecio sucursalListaPrecio)
        {
            if (!sucursalListaPrecioDaoImpl.Delete(sucursalListaPrecio))
            {
                throw new Exception("No fue posible eliminar la lista de precio a la sucursal");
            }
        }

        public List<SucursalListaPrecio> SelectByListaPrecio(int ListaPrecioId)
        {
            return sucursalListaPrecioDaoImpl.SelectByListaPrecio(ListaPrecioId);
        }

        public SucursalListaPrecio SelectBySucursal(int SucursalId)
        {
            return sucursalListaPrecioDaoImpl.SelectByKey(SucursalId, null);
        }


        

        public List<SucursalListaPrecio> SelectAll()
        {
            return sucursalListaPrecioDaoImpl.SelectAll();
        }
    }
}
