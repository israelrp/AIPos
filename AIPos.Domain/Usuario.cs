using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        public int DireccionId { get; set; }
        public int NivelId { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Observaciones { get; set; }
        public bool Eliminado { get; set; }
        public string UserName { get; set; }

        public Direccion Direccion { get; set; }
        public Nivel Nivel { get; set; }

    }
}
