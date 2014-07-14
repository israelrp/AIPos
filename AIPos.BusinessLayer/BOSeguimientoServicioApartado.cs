using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOSeguimientoServicioApartado
    {
        SeguimientoServicioApartadoDaoImpl dao = new SeguimientoServicioApartadoDaoImpl();

        public List<SeguimientoServicioApartado> SelectAll()
        {
            return dao.SelectAll();
        }

        public SeguimientoServicioApartado SelectById(int VentaId)
        {
            return dao.SelectByKey(VentaId);
        }

        public SeguimientoServicioApartado Insert(SeguimientoServicioApartado entity)
        {
            return dao.Insert(entity);
        }

        public SeguimientoServicioApartado Update(SeguimientoServicioApartado entity)
        {
            return dao.Update(entity);
        }

        public void Delete(int VentaId)
        {
            dao.Delete(VentaId);
        }
    }
}
