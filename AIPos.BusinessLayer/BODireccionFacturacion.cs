using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BODireccionFacturacion
    {
        DireccionFacturacionDaoImpl direccionFacturacionDaoImpl = new DireccionFacturacionDaoImpl();


        public DireccionFacturacion Insert(DireccionFacturacion direccionFacturacion)
        {
            return direccionFacturacionDaoImpl.Insert(direccionFacturacion);
        }

        public void Delete(int DireccionId, int ClienteId)
        {
            direccionFacturacionDaoImpl.Delete(DireccionId, ClienteId);
        }

        public List<DireccionFacturacion> SelectByClienteId(int ClienteId)
        {
            return direccionFacturacionDaoImpl.SelectByKeys(ClienteId);
        }
    }
}
