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
        public bool Facturado { get; set; }
        //0.- Pesaje 1.- Cerrada
        public byte Estatus { get; set; }


        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }

        public List<VentaDetalle> VentasDetalle { get; set; }

    }

    public class ReporteCorteCaja
    {
        public int VentaId { get; set; }
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

        public bool Facturado { get; set; }

    }

    public class ReporteVentasProducto
    {
        public int ProductoId { get; set; }
        public string Tipo { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ImporteTotal { get; set; }

    }

    public class ResumenVentasModel
    {
        public List<ConteoVenta> Ventas { get; set; }
        public List<ConteoVenta> Ordenes { get; set; }
        public List<ConteoVenta> Domiciolio { get; set; }
        public List<ConteoVenta> Apartados { get; set; }
        public List<ConteoVenta> Servicios { get; set; }
    }

    public class ConteoVenta
    {
        public DateTime Fecha { get; set; }
        public decimal ImporteDia { get; set; }
        public int Conteo { get; set; }
    }

    public class ReporteUtilidad
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public decimal EntradasCantidad { get; set; }
        public decimal InventariosAnteriorCantidad { get; set; }
        public decimal InventariosActualCantidad { get; set; }
        public decimal VentasCantidad {  get; set;}
        public decimal EntradasImporte { get; set;}
        public decimal InventariosAnteriorImporte { get; set;}
        public decimal InventariosActualImporte { get; set;}
        public decimal VentasImporte { get; set; }
        public decimal SobranteCantidad { get; set; }
        public decimal InventarioCosto { get; set; }
        public decimal InventarioCantidad { get; set; }
        public decimal UtilidadReal { get; set; }
    }
}
