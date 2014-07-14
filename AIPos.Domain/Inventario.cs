﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Inventarios")]
    public class Inventario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public int SucursalId { get; set; }
        public decimal CantidadReal { get; set; }
        public decimal CantidadSistema { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Monto { get; set; }

        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
