using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISCliente" in both code and config file together.
    [ServiceContract]
    public interface ISCliente
    {
        [OperationContract]
        List<Cliente> SelectAll();

        [OperationContract]
        Cliente SelectById(int Id);

        [OperationContract]
        Cliente SelectByCodigo(int Codigo);

        [OperationContract]
        Cliente Insert(Cliente cliente);

        [OperationContract]
        Cliente Update(Cliente cliente);
    }
}
