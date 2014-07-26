using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIPos.WebLayer.Models
{
    public class ResumenVentasChartDataModel
    {
        public string TipoVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int Conteo { get; set; }
        public decimal Total { get; set; }
    }
}