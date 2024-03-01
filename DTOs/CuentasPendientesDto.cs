using System;

namespace ComercialHermanosCastro.DTOs
{
    public class CuentasPendientesDto
    {
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? Total { get; set; }
    }
}
