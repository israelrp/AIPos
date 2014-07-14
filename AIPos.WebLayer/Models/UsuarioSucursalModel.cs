using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIPos.WebLayer.Models
{
    public class UsuarioSucursalModel
    {
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public string Sucursal { get; set; }
    }
}