using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("ListasPrecioProductos")]
    public class ListaPrecioProducto
    {
        [Key, Column(Order = 0)]
        public int ListaPrecioId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductoId { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
    }


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
