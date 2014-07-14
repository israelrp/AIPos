using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISEntrada" in both code and config file together.
    [ServiceContract]
    public interface ISEntrada
    {
        [OperationContract]
        List<Entrada> SelectAll();

        [OperationContract]
        List<Entrada> SelectByDay(int SucursalId, long Day);

        [OperationContract]
        Entrada Insert(Entrada entrada);

        [OperationContract]
        void Delete(int Id);
    }
}
