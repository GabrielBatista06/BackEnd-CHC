using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
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

        public Task<bool> RealizarPago(PagoDto pagoDto)
        {
            return _pagoRepository.RealizarPago(pagoDto);
        }

        public Task<PagosMesDto> TotalIngresosPagos(string? anoActual)
        {
            return _pagoRepository.TotalIngresosPagos(anoActual);
        }
    }
}
