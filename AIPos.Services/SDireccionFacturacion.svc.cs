using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;
using AIPos.BusinessLayer;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SDireccionFacturacion" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SDireccionFacturacion.svc or SDireccionFacturacion.svc.cs at the Solution Explorer and start debugging.
    public class SDireccionFacturacion : ISDireccionFacturacion
    {

        public DireccionFacturacion Insert(DireccionFacturacion direccionFacturacion)
        {
            BODireccionFacturacion boDireccionFacturacion = new BODireccionFacturacion();
            return boDireccionFacturacion.Insert(direccionFacturacion);
        }

        public void Delete(int DireccionId, int ClienteId)
        {
            BODireccionEnvio boDireccionEnvio = new BODireccionEnvio();
            boDireccionEnvio.Delete(DireccionId, ClienteId);
        }
    }
}
