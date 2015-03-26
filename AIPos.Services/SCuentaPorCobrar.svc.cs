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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SCuentaPorCobrar" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SCuentaPorCobrar.svc o SCuentaPorCobrar.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SCuentaPorCobrar : ISCuentaPorCobrar
    {
        public Domain.CuentaPorCobrar NuevaCuentaPorCobrar(Domain.CuentaPorCobrar cuentaPorCobrar)
        {
            cuentaPorCobrar.Fecha = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(cuentaPorCobrar.Fecha);
            cuentaPorCobrar.FechaLimite = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(cuentaPorCobrar.FechaLimite);
            return new BOCuentaPorCobrar().Insert(cuentaPorCobrar);
        }

        public List<CuentaPorCobrar> RecuperarPorCliente(int ClienteId)
        {
            return new BOCuentaPorCobrar().SelectByCliente(ClienteId);
        }

        public List<CuentaPorCobrar> RecuperarPorClienteEstatus(int ClienteId, byte Estatus)
        {
            return new BOCuentaPorCobrar().SelectByClienteEstatus(ClienteId, Estatus);
        }
    }
}
