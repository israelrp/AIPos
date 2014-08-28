using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("MetodosPago")]
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
