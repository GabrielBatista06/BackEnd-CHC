using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface IUsuarioRepository
    {
        Task SaveUser(UsuarioDto usuario);
        Task<bool> ValidateExistence(UsuarioDto usuario);
        Task<UsuarioDto> ValidatePassword(int idUsuario, string passwordAnterior);
        Task UpdatePassword(UsuarioDto usuario);
    }
}
