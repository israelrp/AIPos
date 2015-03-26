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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SSeguimientoServicioApartado" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SSeguimientoServicioApartado.svc or SSeguimientoServicioApartado.svc.cs at the Solution Explorer and start debugging.
    public class SSeguimientoServicioApartado : ISSeguimientoServicioApartado
    {
        public SeguimientoServicioApartado Insert(SeguimientoServicioApartado entity)
        {
            if (entity.FechaLlegadaRepartidor.HasValue)
                entity.FechaLlegadaRepartidor = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaLlegadaRepartidor.Value);
            if (entity.FechaSalidaRepartidor.HasValue)
                entity.FechaSalidaRepartidor = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaSalidaRepartidor.Value);
            entity.FechaSolicitud = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaSolicitud);
            return new BOSeguimientoServicioApartado().Insert(entity);
        }

        public SeguimientoServicioApartado Update(SeguimientoServicioApartado entity)
        {
            if (entity.FechaLlegadaRepartidor.HasValue)
                entity.FechaLlegadaRepartidor = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaLlegadaRepartidor.Value);
            if (entity.FechaSalidaRepartidor.HasValue)
                entity.FechaSalidaRepartidor = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaSalidaRepartidor.Value);
            entity.FechaSolicitud = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaSolicitud);
            return new BOSeguimientoServicioApartado().Update(entity);
        }

        public SeguimientoServicioApartado SelectById(int VentaId)
        {
            return new BOSeguimientoServicioApartado().SelectById(VentaId);
        }

        public int GenerarNuevoFolioEnvio()
        {
            return new BOSeguimientoServicioApartado().GenerarNuevoFolioEnvio();
        }
    }
}
