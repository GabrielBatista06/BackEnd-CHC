using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("CuentasPendientes")]
    public partial class CuentasPendiente
    {
        //public CuentasPendiente()
        //{
        //    Pagos = new HashSet<Pago>();
        //}

        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Total { get; set; }
        public int? DiaPago { get; set; }
        public int? cuotas { get; set; }
        public decimal? valorCuota { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        //public virtual ICollection<Pago> Pagos { get; set; }
    }
}
