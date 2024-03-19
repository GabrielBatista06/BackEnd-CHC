using System;
using System.ComponentModel.DataAnnotations;

namespace ComercialHermanosCastro.Domain.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public string Apodo { get; set;}
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Celular { get; set;}
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEdicion { get; set; }
        //public virtual ICollection<DetalleVentas> DetalleVenta { get; set; }
    }
}
