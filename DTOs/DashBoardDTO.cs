using System.Collections.Generic;

namespace ComercialHermanosCastro.DTOs
{
    public class DashBoardDTO
    {
        public string TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public string? TotalIngresosGeneral { get; set; }
        public string? TotalIngresosGeneralContado { get; set; }
        public string? TotalIngresosGeneralCredito { get; set; }
        public string TotalProductos { get; set; }

        public List<VentasSemanaDto>? VentasUltimaSemana { get; set; }
        
    }
}
