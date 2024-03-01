﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("Venta")]
    public partial class Ventas
    {
        //public Ventas()
        //{
        //    DetalleVenta = new HashSet<DetalleVentas>();
        //}
        [Key]
        public int IdVenta { get; set; }
        [ForeignKey("usuario")]
        public int? Usuario { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoPago { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaPago { get; set; }
        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVentas> DetalleVenta { get; set; }
    }
}
