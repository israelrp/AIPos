using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("DireccionesEnvio")]
    public class DireccionEnvio
    {
        [Key, Column(Order=0)]
        public int DireccionId { get; set; }
        [Key, Column(Order = 1)]
        public int ClienteId { get; set; }

        public Direccion Direccion { get; set; }
        public Cliente Cliente { get; set; }
    }
}
