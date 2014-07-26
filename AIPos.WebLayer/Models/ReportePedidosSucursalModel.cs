using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIPos.WebLayer.Models
{
    public class ReportePedidosSucursalModel
    {
        public string Sucursal { get; set; }
        public string Producto { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
    }
}