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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SServicioApartado" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SServicioApartado.svc or SServicioApartado.svc.cs at the Solution Explorer and start debugging.
    public class SServicioApartado : ISServicioApartado
    {
        public ServicioApartado Insert(ServicioApartado servicioApartado)
        {
            return new BOServicioApartado().Insert(servicioApartado);
        }

        public List<ServicioApartado> SelectAllPendientes()
        {
            return new BOServicioApartado().SelectAll().Where(x => x.EstatusId == 2).ToList();
        }

        public ServicioApartado Update(ServicioApartado servicioApartado)
        {
            return new BOServicioApartado().Update(servicioApartado);
        }


        public List<ReporteServicioApartado> RecuperarReporteServicioApartado(int SucursalId, int? EstatusId)
        {
            return new BOServicioApartado().RecuperarReporteServicioApartado(SucursalId, EstatusId);
        }


        public ServicioApartado SelectById(int VentaId)
        {
            return new BOServicioApartado().SelectById(VentaId);
        }
    }
}
