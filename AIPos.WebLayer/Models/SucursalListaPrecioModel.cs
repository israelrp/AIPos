using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIPos.WebLayer.Models
{
    public class SucursalListaPrecioModel
    {
        public string Sucursal { get; set; }
        public string ListaPrecio { get; set; }
        public int SucursalId { get; set; }
        public int? ListaPrecioId { get; set; }
    }
}