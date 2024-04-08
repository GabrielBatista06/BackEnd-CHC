using System.Collections.Generic;

namespace ComercialHermanosCastro.DTOs
{
    public class PagosMesDto
    {
        public string? TotalCobrosGeneral { get; set; }
        public List<PagosMes>? PagosMesTotal { get; set; }
    }

    public class PagosMes 
    {
        public string? Mes { get; set; }
        public string? Total { get; set; }
    }
}
