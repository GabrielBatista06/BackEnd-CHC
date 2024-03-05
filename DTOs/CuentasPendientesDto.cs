using System;

namespace ComercialHermanosCastro.DTOs
{
    public class CuentasPendientesDto
    {
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? DiaPago { get; set; }
        public decimal? Total { get; set; }
    }
}
