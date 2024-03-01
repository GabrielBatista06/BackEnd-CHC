using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface IDashBoardService
    {
        Task<DashBoardDTO> Resumen();
    }
}
