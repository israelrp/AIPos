using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("ConfiguracionGeneral")]
    public class ConfiguracionGeneral
    {
        public int Id { get; set; }
        public bool ActivarBascula { get; set; }
        public bool ActivarTicketPesaje { get; set; }
        public bool ActivarFotoProductoPOS {get; set;}
        public byte NumeroCopiasTicketVenta { get; set; }
        public byte DecimalesPrecioProducto { get; set; }
        public byte DecimalesCantidad { get; set; }
        public byte[] LogoTicket { get; set; }
        public string TituloTicket { get; set; }
        public string AgradecimientoTicket { get; set; }
        public string LeyendaFicalTicket { get; set; }
        public bool ActivarImprimirFechaHoraTicket { get; set; }
    }
}
