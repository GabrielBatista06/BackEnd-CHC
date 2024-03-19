namespace ComercialHermanosCastro.DTOs
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Tamano { get; set; }
        public int? Stock { get; set; }
        public string? Precio { get; set; }
        public int? EsActivo { get; set; }
    }
}
