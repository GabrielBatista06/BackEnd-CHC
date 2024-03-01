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
        public DateTime? FechaPago { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        //public virtual ICollection<Pago> Pagos { get; set; }
    }
}
