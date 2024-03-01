using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("DetalleVenta")]
    public partial class DetalleVentas
    {
        [Key]

        public int IdDetalleVenta { get; set; }
        public int? IdVenta { get; set; }
        public int? IdCliente { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
        public virtual Ventas IdVentaNavigation { get; set; }
    }
}
