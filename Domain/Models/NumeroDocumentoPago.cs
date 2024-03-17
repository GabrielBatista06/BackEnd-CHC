using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ComercialHermanosCastro.Domain.Models
{
    [Table("NumeroDocumentoPago")]

    public class NumeroDocumentoPago
    {

        [Key]
        public int IdNumeroDocumento { get; set; }
        [Column("ultimo_Numero")]
        public int UltimoNumero { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}
