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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Producto" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Producto.svc or Producto.svc.cs at the Solution Explorer and start debugging.
    public class SProducto : ISProducto
    {

        public List<Producto> SelectAllProductos()
        {
            return new BOProducto().SelectAll();
        }

        public Producto SelectByCodigo(string Codigo)
        {
            BOProducto boProducto = new BOProducto();
            return boProducto.SelectByCodigo(Codigo);
        }

        public Producto SelectById(int Id)
        {
            return new BOProducto().SelectById(Id);
        }
    }
}
