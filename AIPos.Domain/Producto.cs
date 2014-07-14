using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AIPos.Domain
{
    [DataContract]
    [Table("Productos")]
    public class Producto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UnidadId { get; set; }
        [DataMember]
        public int TipoId { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
        [DataMember]
        public bool SePesa { get; set; }
        [DataMember]
        public decimal Impuesto { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Fotografia { get; set; }
        [DataMember]
        public bool Eliminado { get; set; }
        [DataMember]
        public decimal Descuento { get; set; }

        public Unidad Unidad { get; set; }
        public Tipo Tipo { get; set; }
    }
}
