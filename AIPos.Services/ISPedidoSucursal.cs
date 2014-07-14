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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISPedidoSucursal" in both code and config file together.
    [ServiceContract]
    public interface ISPedidoSucursal
    {
        [OperationContract]
        PedidoSucursal Insert(PedidoSucursal entity);

        [OperationContract]
        PedidoSucursal Update(PedidoSucursal entity);

        [OperationContract]
        void Delete(int Id);

        [OperationContract]
        List<PedidoSucursal> SelectBySucursalFecha(int SucursalId, long Day);
    }
}
