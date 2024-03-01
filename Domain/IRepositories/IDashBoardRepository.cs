using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface IDashBoardRepository
    {
        Task<DashBoardDTO> Resumen();
    }
}
