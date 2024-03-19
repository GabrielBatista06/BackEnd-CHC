using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("NumeroDocumento")]
    public partial class NumeroDocumento
    {
        [Key]
        public int IdNumeroDocumento { get; set; }
        [Column("ultimo_Numero")]
        public int UltimoNumero { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
