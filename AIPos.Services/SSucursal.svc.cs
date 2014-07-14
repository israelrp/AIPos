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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SSucursal" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SSucursal.svc or SSucursal.svc.cs at the Solution Explorer and start debugging.
    public class SSucursal : ISSucursal
    {

        public List<Sucursal> SelectAll()
        {
            return new BOSucursal().SelectAll();
        }

        public Sucursal SelectById(int Id)
        {
            return new BOSucursal().SelectById(Id);
        }
    }
}
