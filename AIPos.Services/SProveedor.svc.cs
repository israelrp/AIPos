using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.BusinessLayer;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SProveedor" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SProveedor.svc or SProveedor.svc.cs at the Solution Explorer and start debugging.
    public class SProveedor : ISProveedor
    {


        public List<Domain.Proveedor> SelectAll()
        {
            return new BOProveedor().SelectAll();
        }

        public Domain.Proveedor SelectByCodigo(string Codigo)
        {
            return new BOProveedor().SelectByCodigo(Codigo);
        }

        public Domain.Proveedor SelectById(int Id)
        {
            return new BOProveedor().SelectById(Id);
        }
    }
}
