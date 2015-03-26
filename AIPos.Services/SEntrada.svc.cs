using System;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SEntrada" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SEntrada.svc or SEntrada.svc.cs at the Solution Explorer and start debugging.
    public class SEntrada : ISEntrada
    {

        public List<Domain.Entrada> SelectAll()
        {
            BOEntrada boEntrada = new BOEntrada();
            return boEntrada.SelectAll();
        }

        public List<Domain.Entrada> SelectByDay(int SucursalId, long Day)
        {
            BOEntrada boEntrada = new BOEntrada();
            DateTime fecha=DateTime.FromFileTimeUtc(Day);
            //if (!DateTime.TryParse(Day, out fecha))
            //{
            //    throw new Exception("El formato de la fecha es inválido: " + Day + ". La hora del servicio es: " + DateTime.Now.ToString());
            //}
            return boEntrada.SelectByDay(SucursalId,fecha);
        }

        public Domain.Entrada Insert(Domain.Entrada entrada)
        {
            BOEntrada boEntrada = new BOEntrada();
            entrada.Fecha = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entrada.Fecha.ToUniversalTime());
            return boEntrada.Insert(entrada);
        }

        public void Delete(int Id)
        {
            BOEntrada boEntrada = new BOEntrada();
            boEntrada.Delete(Id);
        }
    }
}
