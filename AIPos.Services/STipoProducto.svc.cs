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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "STipoProducto" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select STipoProducto.svc or STipoProducto.svc.cs at the Solution Explorer and start debugging.
    public class STipoProducto : ISTipoProducto
    {

        public List<TipoProducto> SelectAll()
        {
            return new BOTipoProducto().SelectAll();
        }

        public TipoProducto SelectById(int Id)
        {
            return new BOTipoProducto().SeectById(Id);
        }
    }
}
