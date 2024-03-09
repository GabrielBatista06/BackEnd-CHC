using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(UsuarioDto usuario);
    }
}
