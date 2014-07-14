using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("ListasPrecio")]
    public class ListaPrecio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
