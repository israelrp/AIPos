using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IISVenta" in both code and config file together.
    [ServiceContract]
    public interface IISVenta
    {
        [OperationContract]
        Venta Insert(Venta venta);

        [OperationContract]
        int GenerarFolioVenta(int SucursalId);

        [OperationContract]
        int GenerarFolioCancelado(int SucursalId, long Fecha);

        [OperationContract]
        int GenerarFolioCanceladoApartado(int SucursalId, long Fecha);

        [OperationContract]
        int GenerarFolioCanceladoDomicilio(int SucursalId, long Fecha);

        [OperationContract]
        int GenerarFolioCanceladoServicio(int SucursalId, long Fecha);

        [OperationContract]
        List<ReporteCorteCaja> RecuperarCorteCaja(long FechaInicio, long FechaFin, int? SucursalId);
    }
}
