using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIPos.WebLayer.Models
{
    public class ClienteListaPrecioModel
    {
        public string Cliente { get; set; }
        public string ListaPrecio { get; set; }
        public int ClienteId { get; set; }
        public int? ListaPrecioId { get; set; }
    }
}