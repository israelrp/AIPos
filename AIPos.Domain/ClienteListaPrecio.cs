using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("ClientesListasPrecio")]
    public class ClienteListaPrecio
    {
        [Key, Column(Order = 0)]
        public int ClienteId { get; set; }
        [Key, Column(Order = 1)]
        public int ListaPrecioId { get; set; }
    }
}
