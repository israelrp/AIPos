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
    public class BOEstatusServicioApartado
    {
        EstatusServicioApartadoDaoImpl estatusServicioApartadoDaoImpl = new EstatusServicioApartadoDaoImpl();

        public List<EstatusServicioApartado> SelectAll()
        {
            return estatusServicioApartadoDaoImpl.SelectAll();
        }

    }
}
