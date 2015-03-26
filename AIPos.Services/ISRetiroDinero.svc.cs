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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ISRetiroDinero" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ISRetiroDinero.svc or ISRetiroDinero.svc.cs at the Solution Explorer and start debugging.
    public class ISRetiroDinero : IISRetiroDinero
    {

        public void Delete(int Id)
        {
            BORetiroDinero boRetiroDinero = new BORetiroDinero();
            boRetiroDinero.Delete(Id);
        }

        public Domain.RetiroDinero Insert(Domain.RetiroDinero retiroDinero)
        {
            BORetiroDinero boRetiroDinero = new BORetiroDinero();
            retiroDinero.Fecha = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(retiroDinero.Fecha);
            return boRetiroDinero.Insert(retiroDinero);
        }

        public Domain.RetiroDinero Update(Domain.RetiroDinero retiroDinero)
        {
            BORetiroDinero boRetiroDinero = new BORetiroDinero();
            retiroDinero.Fecha = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(retiroDinero.Fecha);
            return boRetiroDinero.Update(retiroDinero);
        }

        public List<Domain.RetiroDinero> SelectAllByFechaSucursal(long Dia, int SucursalId)
        {
            BORetiroDinero boRetiroDinero = new BORetiroDinero();
            return boRetiroDinero.SelectAllByFechaSucursal(SucursalId, BusinessLayer.Tools.TimeConverter.GetDateTimeMexico( DateTime.FromFileTimeUtc(Dia)));
        }
    }
}
