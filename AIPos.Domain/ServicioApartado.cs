using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("ServiciosApartados")]
    public class ServicioApartado
    {
        [Key]
        public int VentaId { get; set; }
        public int DireccionEnvioId { get; set; }
        public int EstatusId { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Especificaciones { get; set; }
        public decimal Anticipo { get; set; }
        /// <summary>
        /// 0: Domicilio, 1: Apartado, 2: Servicio
        /// </summary>
        public byte Tipo { get; set; }

        public Venta Venta { get; set; }
        public Direccion DireccionEnvio { get; set; }
        public EstatusServicioApartado Estatus { get; set; }
    }

    public class ReporteServicioApartado
    {
        public int VentaId { get; set; }
        public int EstatusServicioApartadoId { get; set; }
        public decimal Total { get; set; }
        public int FolioCancelado { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Especificaciones { get; set; }
        public decimal Anticipo { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string DireccionEnvio { get; set; }
        public string Cajero { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaSalidaRepartidor { get; set; }
        public DateTime? FechaLlegadaRepartidor { get; set; }
        public string Repartidor { get; set; }
    }
}
