using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISCuentaPorCobrar" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ISCuentaPorCobrar
    {
        [OperationContract]
        CuentaPorCobrar NuevaCuentaPorCobrar(CuentaPorCobrar cuentaPorCobrar);

        [OperationContract]
        List<CuentaPorCobrar> RecuperarPorCliente(int ClienteId);

        [OperationContract]
        List<CuentaPorCobrar> RecuperarPorClienteEstatus(int ClienteId, byte Estatus);
    }
}
