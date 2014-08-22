using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IISRetiroDinero" in both code and config file together.
    [ServiceContract]
    public interface IISRetiroDinero
    {
        [OperationContract]
        void Delete(int Id);

        [OperationContract]
        RetiroDinero Insert(RetiroDinero retiroDinero);

        [OperationContract]
        RetiroDinero Update(RetiroDinero retiroDinero);

        [OperationContract]
        List<RetiroDinero> SelectAllByFechaSucursal(long Dia, int SucursalId);


    }
}
