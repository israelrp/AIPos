using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("PedidosSucursal")]
    public class PedidoSucursal
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public int UnidadId { get; set; }
        public string Producto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Cantidad { get; set; }

        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }
        public Unidad Unidad { get; set; }
    }
}
