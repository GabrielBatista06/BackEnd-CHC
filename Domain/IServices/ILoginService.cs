using ComercialHermanosCastro.Domain.Models;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}
