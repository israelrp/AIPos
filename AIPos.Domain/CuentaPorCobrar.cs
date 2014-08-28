using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("CuentasPorCobrar")]
    public class CuentaPorCobrar
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int VentaId { get; set; }
        public decimal Importe { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaLimite { get; set; }
        public byte Estatus { get; set; } //1.- Pendiente 2.- Pagada 3.- Vencida
    }
}
