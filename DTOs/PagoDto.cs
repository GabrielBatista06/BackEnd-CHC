using System;

namespace ComercialHermanosCastro.DTOs
{
    public class PagoDto
    {
        public int? UsuarioCobro { get; set; }
        public int? IdcuentaPendiente { get; set; }
        public decimal? BalanceAnterior { get; set; }
        public decimal? MontoPagado { get; set; }
        public string TipoPago { get; set; }
    }
}
