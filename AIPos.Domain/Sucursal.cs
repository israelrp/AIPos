using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Sucursales")]
    public class Sucursal
    {
        public int Id { get; set; }
        public int DireccionId  { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string FraseTicket { get; set; }

        public Direccion Direccion { get; set; }
    }
}
