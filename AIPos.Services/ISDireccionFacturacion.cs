using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISDireccionFacturacion" in both code and config file together.
    [ServiceContract]
    public interface ISDireccionFacturacion
    {
        [OperationContract]
        DireccionFacturacion Insert(DireccionFacturacion direccionFacturacion);

        [OperationContract]
        void Delete(int DireccionId, int ClienteId);
    }
}
