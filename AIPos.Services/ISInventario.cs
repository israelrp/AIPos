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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISInventario" in both code and config file together.
    [ServiceContract]
    public interface ISInventario
    {
        [OperationContract]
        Inventario Insert(Inventario entity);

        [OperationContract]
        Inventario Update(Inventario entity);

        [OperationContract]
        void Delete(int Id);

        [OperationContract]
        List<Inventario> SelectBySucursalFecha(int SucursalId, long Day);


    }
}
