using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("Unidades")]
    public class Unidad
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }
    }
}
