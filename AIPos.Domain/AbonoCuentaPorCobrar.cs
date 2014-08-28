using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("AbonosCuentaPorCobrar")]
    public class AbonoCuentaPorCobrar
    {
        public int Id { get; set; }
        public int CuentaPorCobrarId { get; set; }
        public int UsuarioId { get; set; }
        public int MetodoPagoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public string Observaciones { get; set; }
    }
}
