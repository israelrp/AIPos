using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("SeguimientosServiciosApartados")]
    public class SeguimientoServicioApartado
    {
        [Key]
        public int VentaId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaSalidaRepartidor { get; set; }
        public DateTime? FechaLlegadaRepartidor { get; set; }
        public string Repartidor { get; set; }
    }
}
