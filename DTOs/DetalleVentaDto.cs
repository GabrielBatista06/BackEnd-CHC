﻿namespace ComercialHermanosCastro.DTOs
{
    public class DetalleVentaDto
    {
        //public int? IdCliente { get; set; }
        //public string NombreCliente { get; set; }
        public int? IdProducto { get; set; }
        public string DescripcionProducto { get; set; }         
        public int? Cantidad { get; set; }
        public string? Precio { get; set; }
        public string? Total { get; set; }
    }
}
