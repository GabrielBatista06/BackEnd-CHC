using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("CuentasPendientes")]
    public partial class CuentasPendiente
    {
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Total { get; set; }
        public int? DiaPago { get; set; }
        public int? cuotas { get; set; }
        public decimal? valorCuota { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public string? numeroDocumento { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
