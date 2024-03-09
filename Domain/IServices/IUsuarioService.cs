using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(UsuarioDto usuario);
        Task<bool> ValidateExistence(UsuarioDto usuario);
        Task<UsuarioDto> ValidatePassword(int idUsuario, string passwordAnterior);
        Task UpdatePassword(UsuarioDto usuario);
    }
}
