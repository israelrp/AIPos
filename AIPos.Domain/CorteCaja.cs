using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("CortesCaja")]
    public class CorteCaja
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalMostrador { get; set; }
        public decimal TotalDomicilio { get; set; }
        public decimal TotalServicios { get; set; }
        public decimal TotalApartados { get; set; }
        public decimal TotalRetiros { get; set; }
        public decimal TotalAbonosServicios { get; set; }
        public decimal TotalAbonosApartados { get; set; }
        public decimal TotalCancelados { get; set; }
        public decimal TotalCaja { get; set; }
        public decimal TotalCambio { get; set; }
        public string QuienRetira { get; set; }
        public decimal CorteEntregado { get; set; }
        public decimal Diferencia { get; set; }        
        public int TotalTickectsDomicilio { get; set; }
        public int TotalTickectsMostrador { get; set; }

        public decimal TotalVentas { get; set; }

        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
