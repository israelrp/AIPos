﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;
using AIPos.BusinessLayer;
using System.Globalization;

namespace AIPos.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SEstatusServicioApartado" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SEstatusServicioApartado.svc o SEstatusServicioApartado.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SEstatusServicioApartado : ISEstatusServicioApartado
    {

        public List<Domain.EstatusServicioApartado> SelectAll()
        {
            return new BOEstatusServicioApartado().SelectAll();
        }
    }
}
