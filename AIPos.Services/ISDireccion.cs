using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISDireccion" in both code and config file together.
    [ServiceContract]
    public interface ISDireccion
    {
        [OperationContract]
        Direccion Insert(Direccion direccion);

        [OperationContract]
        Direccion Update(Direccion direccion);

        [OperationContract]
        List<Direccion> SelectByCliente(int ClienteId);

        [OperationContract]
        Direccion SelectById(int Id);
    }
}
