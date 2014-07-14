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
    public class BOUnidad
    {
        UnidadDaoImpl unidadDaoImp = new UnidadDaoImpl();

        public Unidad Insert(Unidad unidad)
        {
            unidad = unidadDaoImp.Insert(unidad);
            return unidad;
        }

        public void Delete(int id)
        {
            if (!unidadDaoImp.Delete(id))
            {
                throw new Exception("No fue posible eliminar la unidad");
            }
        }

        public Unidad Update(Unidad unidad)
        {
            unidad = unidadDaoImp.Update(unidad);
            return unidad;
        }

        public List<Unidad> SelectAll()
        {
            return unidadDaoImp.SelectAll();
        }

        public Unidad SelectById(int id)
        {
            return unidadDaoImp.SelectByKey(id);
        }
    }
}
