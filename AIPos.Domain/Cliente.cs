﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIPos.Domain
{
    [Table("Clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        public int SucursalRegistroId { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public int Codigo { get; set; }
        public bool Eliminado { get; set; }
        public decimal Descuento { get; set; }
        public string RazonSocial { get; set; }
        public bool CreditoActivo { get; set; }
        public decimal LimiteCredito { get; set; }
        public int DiaPago { get; set; }

        public Sucursal SucursalRegistro { get; set; }
        public Direccion DireccionFacturaion { get; set; }
    }
}
