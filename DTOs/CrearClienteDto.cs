﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ComercialHermanosCastro.DTOs
{
    public class CrearClienteDto
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Apodo { get; set; }
        public string Cedula { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEdicion { get; set; }
    }
}
