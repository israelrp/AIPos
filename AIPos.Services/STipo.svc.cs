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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "STipo" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select STipo.svc or STipo.svc.cs at the Solution Explorer and start debugging.
    public class STipo : ISTipo
    {


        public Tipo SelectById(int Id)
        {
            return new BOTipo().SelectById(Id);
        }


        public List<Tipo> SelectAll()
        {
            return new BOTipo().SelectAll();
        }
    }
}
