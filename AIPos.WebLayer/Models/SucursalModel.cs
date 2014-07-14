using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIPos.Domain;

namespace AIPos.WebLayer.Models
{
    public class SucursalModel : Sucursal
    {
        public string Calle { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
    }
}