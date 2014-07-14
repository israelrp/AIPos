using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Proveedores")]
    public class Proveedor
    {
        public int Id { get; set; }
        public int DireccionId { get; set; }
        public string RazonSocial { get; set; }
        public string Rfc { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Codigo { get; set; }
        public string Email { get; set; }
        public bool Eliminado { get; set; }

        public Direccion Direccion { get; set; }
    }
}
