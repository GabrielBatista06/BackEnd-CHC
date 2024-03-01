using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("Pagos")]
    public  class Pago
    {
        public int Id { get; set; }
        public int? usuarioCobro { get; set; }
        public int? idcuentaPendiente { get; set; }
        public decimal? balanceAnterior { get; set; }
        public decimal? montoPagado { get; set; }
        public DateTime? fechaPago { get; set; }

        public virtual CuentasPendiente IdcuentaPendienteNavigation { get; set; }
        public virtual Usuario UsuarioCobroNavigation { get; set; }
    }
}
