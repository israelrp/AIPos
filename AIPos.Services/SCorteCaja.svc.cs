using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.BusinessLayer;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SCorteCaja" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SCorteCaja.svc or SCorteCaja.svc.cs at the Solution Explorer and start debugging.
    public class SCorteCaja : ISCorteCaja
    {
        public CorteCaja Insert(CorteCaja entity)
        {
            BOCorteCaja boCorteCaja = new BOCorteCaja();
            return boCorteCaja.Insert(entity);
        }
    }
}
