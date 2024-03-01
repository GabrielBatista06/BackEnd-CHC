using System.Collections.Generic;

namespace ComercialHermanosCastro.DTOs
{
    public class DashBoardDTO
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int TotalProductos { get; set; }

        public List<VentasSemanaDto>? VentasUltimaSemana { get; set; }
        
    }
}
