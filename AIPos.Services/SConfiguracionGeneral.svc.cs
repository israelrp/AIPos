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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SConfiguracionGeneral" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SConfiguracionGeneral.svc o SConfiguracionGeneral.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SConfiguracionGeneral : ISConfiguracionGeneral
    {
        public ConfiguracionGeneral RecuperarConfiguracion()
        {
            return new BOConfiguracionGeneral().ObtenerConfiguracion();
        }
    }
}
