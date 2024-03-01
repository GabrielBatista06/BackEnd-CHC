using ComercialHermanosCastro.Domain.Models;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface IVentaGenericRepository : IGenericRepository<Ventas>
    {
        Task<Ventas> Registrar(Ventas modelo);
    }
}
