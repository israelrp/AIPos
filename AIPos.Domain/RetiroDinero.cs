using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("RetirosDinero")]
    public class RetiroDinero
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsCorteCaja { get; set; }

        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }
    }

    public class ReporteRetiroDinero
    {
        public int RetiroDineroId { get; set; }
        public string Usuario { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsCorteCaja { get; set; }
    }
}
