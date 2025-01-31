using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface IPagoService
    {
        Task<bool> RealizarPago(PagoDto pagoDto);
        Task<PagosMesDto> TotalIngresosPagos(string? anoActual);
        Task<DashBoardPagosSemanaDto> Resumen();
    }
}
