﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Entradas")]
    public class Entrada
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public int ProductoId { get; set; }
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public int TipoProductoId { get; set; }
        public decimal CantidadReal { get; set; }
        public int TotalPiezas { get; set; }
        public DateTime Fecha { get; set; }
        public decimal CantidadProveedor { get; set; }
        public decimal Importe { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Diferencia { get; set; }

        public Proveedor Proveedor { get; set; }
        public Producto Producto { get; set; }
        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }
        public TipoProducto TipoProducto { get; set; }
    }
}
