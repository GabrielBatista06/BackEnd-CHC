namespace ComercialHermanosCastro.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string? Rol { get; set; }
    }
}
