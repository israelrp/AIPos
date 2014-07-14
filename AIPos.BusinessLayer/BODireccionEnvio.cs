using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BODireccionEnvio
    {
        DireccionEnvioDaoImpl direccionEnvioDaoImpl = new DireccionEnvioDaoImpl();

        public DireccionEnvio Insert(DireccionEnvio direccionEnvio)
        {
            return direccionEnvioDaoImpl.Insert(direccionEnvio);
        }

        public void Delete(int DireccionId, int ClienteId)
        {
            direccionEnvioDaoImpl.Delete(DireccionId,ClienteId);
        }
    }
}
