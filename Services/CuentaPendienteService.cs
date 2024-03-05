using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Services
{
    public class CuentaPendienteService : ICuentaPendienteService
    {
        private readonly ICuentaPendienteRepository _repository;

        public CuentaPendienteService(ICuentaPendienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto)
        {
            return await _repository.GenerarCuentaPendiente(cuentasPendientesDto);
        }

        public async Task<List<CuentasPendientesDto>> Lista()
        {
            return await _repository.Lista();
        }
    }
}
