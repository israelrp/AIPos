using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIPos.WebLayer.Models
{
    public class ListaPrecioProductoModel
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioLista { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioDescuento { get; set; }
        public int ListaPrecioId { get; set; }
        public bool EsNuevo { get; set; }
    }
}