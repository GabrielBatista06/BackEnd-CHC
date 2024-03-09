using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Services
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Usuario> ValidateUser(UsuarioDto usuario)
        {
            return await _loginRepository.ValidateUser(usuario);

        }
    }
}
