using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPos.Domain
{
    [Table("UsuariosSucursal")]
    public class UsuarioSucursal
    {
        [Key, Column(Order=0)]
        public int SucursalId { get; set; }
        [Key, Column(Order = 1)]
        public int UsuarioId { get; set; }

        public Sucursal Sucursal { get; set; }
        public Usuario Usuario { get; set; }
    }
}
