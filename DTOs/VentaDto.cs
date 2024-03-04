using ComercialHermanosCastro.Domain.Models;
using System;
using System.Collections.Generic;

namespace ComercialHermanosCastro.DTOs
{
    public class VentaDto
    {
        public int IdVenta { get; set; }
        public int? IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int? Usuario { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoPago { get; set; }
        public string FechaRegistro { get; set; }
        public string Total { get; set; }
        public string? FechaPago { get; set; }
        public virtual ICollection<DetalleVentaDto> DetalleVenta { get; set; }
    }
}
