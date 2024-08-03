using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.Domain.Models;
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

        public async Task<int> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto)
        {
            return await _repository.GenerarCuentaPendiente(cuentasPendientesDto);
        }

        public async Task<List<CuentasPendientesDto>> Lista()
        {
            return await _repository.Lista();
        }

        public async Task<List<CuentasPendientesAtrasadasDto>> ListaCuentasAtraso()
        {
            return await _repository.ListaCuentasAtraso();
        }

        public async Task<TotalPendienteGeneralDto> Resumen()
        {
            return await _repository.Resumen();
        }

    }
}
