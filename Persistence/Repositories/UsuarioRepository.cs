using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        public UsuarioRepository(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task SaveUser(UsuarioDto    usuario)
        {
            _context.Add(_mapper.Map<Usuario>(usuario));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(UsuarioDto usuario)
        {
            var validateExistence = await _context.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);

            return validateExistence;
        }

        public async Task<UsuarioDto   > ValidatePassword(int idUsuario, string passwordAnterior)
        {
            var usuario = await _context.Usuarios.AsNoTracking().Where(x => x.Id == idUsuario && x.Password == passwordAnterior).FirstOrDefaultAsync();
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task UpdatePassword(UsuarioDto usuario)
        {
            _context.Update(_mapper.Map<Usuario>(usuario));

            await _context.SaveChangesAsync();
        }
    }
}
