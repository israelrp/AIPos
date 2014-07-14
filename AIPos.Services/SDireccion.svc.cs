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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SDireccion" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SDireccion.svc or SDireccion.svc.cs at the Solution Explorer and start debugging.
    public class SDireccion : ISDireccion
    {

        public Direccion Insert(Direccion direccion)
        {
            return new BODireccion().Insert(direccion);
        }

        public Direccion Update(Direccion direccion)
        {
            return new BODireccion().Update(direccion);
        }

        public List<Direccion> SelectByCliente(int ClienteId)
        {
            return new BODireccion().SelectByClienteId(ClienteId);
        }


        public Direccion SelectById(int Id)
        {
            return new BODireccion().SelectById(Id);
        }
    }
}
