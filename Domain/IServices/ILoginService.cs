using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(UsuarioDto usuario);
    }
}
