using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public async Task<bool> RealizarPago(PagoDto pagoDto)
        {
            return await _pagoRepository.RealizarPago(pagoDto);
        }

        public async Task<PagosMesDto> TotalIngresosPagos(string? anoActual)
        {
            return await _pagoRepository.TotalIngresosPagos(anoActual);
        }

        public async Task<DashBoardPagosSemanaDto> Resumen()
        {
             return await _pagoRepository.Resumen();
        }

    }
}
