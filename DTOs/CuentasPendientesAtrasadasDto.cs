using System;

namespace ComercialHermanosCastro.DTOs
{
    public class CuentasPendientesAtrasadasDto
    {
        public int? Id { get; set; }
        public int? IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Apodo { get; set; }
        public decimal? Total { get; set; }
        public int? DiaPago { get; set; }
        public int? Cuotas { get; set; }
        public decimal? ValorCuota { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? NumeroDocumento { get; set; }
        public DateTime? fechaUltimoPago { get; set; }
        
    }
}
