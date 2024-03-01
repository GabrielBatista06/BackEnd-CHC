using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("Producto")]
    public partial class Producto
    {
        //public Producto()
        //{
        //    DetalleVenta = new HashSet<DetalleVentas>();
        //}
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Tamano { get; set; }
        public int? Stock { get; set; }
        public decimal? Precio { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaEdicion { get; set; }

       public virtual ICollection<DetalleVentas> DetalleVenta { get; set; }
    }
}
