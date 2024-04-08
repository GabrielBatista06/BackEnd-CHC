using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface IPagoRepository
    {
        Task<bool> RealizarPago(PagoDto pagoDto);
        Task<PagosMesDto> TotalIngresosPagos(string? anoActual);
    }
}
