using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Ventas")]
    public class Venta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public DateTime Fecha { get; set; }
        public int Folio { get; set; }
        public decimal Recibio { get; set; }
        public decimal Cambio { get; set; }
        public int FolioCancelado { get; set; }
        public bool Cancelado { get; set; }
        public decimal Total { get; set; }
        public bool RequiereFactura { get; set; }

        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }

        public List<VentaDetalle> VentasDetalle { get; set; }

    }

    public class ReporteCorteCaja
    {
        public string Cliente { get; set; }
        public string Sucursal { get; set; }
        public int SucursalId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Total { get; set; }
        public decimal? Recibio { get; set; }
        public decimal? Cambio { get; set; }
        public decimal? Anticipo { get; set; }
        public int FolioCancelado { get; set; }
        public int Folio { get; set; }
        public string Usuario { get; set; }
        public string Estatus { get; set; }
        public string Tipo { get; set; }
        public bool Cancelado { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Especificaciones { get; set; }
        public string DireccionEnvio { get; set; }

    }
}
