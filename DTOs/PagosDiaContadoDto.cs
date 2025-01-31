using System.Collections.Generic;

namespace ComercialHermanosCastro.DTOs
{
    public class PagosDiaContadoDto
    {
        public string? TotalCobrosGeneral { get; set; }
        public List<PagosContadoDto>? PagosContado { get; set; }
        public class PagosContadoDto
        {
            public string? NombreCliente { get; set; }
            public string? ApellidosCliente { get; set; }
            public decimal? TotalVenta { get; set; }
            public string? FechaRegistro { get; set; }
        }

    }
}
