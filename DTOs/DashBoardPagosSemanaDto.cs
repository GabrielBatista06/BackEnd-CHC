using System.Collections.Generic;

namespace ComercialHermanosCastro.DTOs
{
    public class DashBoardPagosSemanaDto
    {
        public string TotalPagos { get; set; }
        public string? TotalIngresos { get; set; }

        public List<PagosUltimaSemanaDto>? PagosUltimaSemana { get; set; }
    }

    public class PagosUltimaSemanaDto
    {
        public string? Fecha { get; set; }
        public decimal? Total { get; set; }
    }
}
