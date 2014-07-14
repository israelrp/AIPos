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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SListaPrecio" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SListaPrecio.svc or SListaPrecio.svc.cs at the Solution Explorer and start debugging.
    public class SListaPrecio : ISListaPrecio
    {

        public List<ListaPrecio> SelectAll()
        {
            return new BOListaPrecio().SelectAll();
        }
        
        public ListaPrecio RecuperarListaPrecioDeSucursal(int SucursalId)
        {
            return new BOListaPrecio().RecuperarListaPrecioDeSucursal(SucursalId);
        }
    }
}
