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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SCliente" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SCliente.svc or SCliente.svc.cs at the Solution Explorer and start debugging.
    public class SCliente : ISCliente
    {

        public List<Domain.Cliente> SelectAll()
        {
            return new BOCliente().SelectAll();
        }

        public Domain.Cliente SelectById(int Id)
        {
            return new BOCliente().SelectById(Id);
        }

        public Domain.Cliente SelectByCodigo(int Codigo)
        {
            return new BOCliente().SelectByCodigo(Codigo);
        }

        public Domain.Cliente Insert(Domain.Cliente cliente)
        {
            return new BOCliente().Insert(cliente);
        }

        public Domain.Cliente Update(Domain.Cliente cliente)
        {
            return new BOCliente().Update(cliente);
        }
    }
}
