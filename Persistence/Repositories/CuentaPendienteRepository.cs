using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class CuentaPendienteRepository : ICuentaPendienteRepository
    {
        private readonly IGenericRepository<CuentasPendiente> _cuentaRepository;
        private readonly IMapper _mapper;

        public CuentaPendienteRepository(IGenericRepository<CuentasPendiente> cuentaRepository, 
                                                                        IMapper mapper)
        {
            _cuentaRepository = cuentaRepository;
            _mapper = mapper;
        }

        public async Task<bool> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto)
        {
            try
            {
                var crearCuenta = await _cuentaRepository.Crear(_mapper.Map<CuentasPendiente>(cuentasPendientesDto));

                if (crearCuenta.Id == 0)
                {
                    throw new TaskCanceledException("No se pudo generar la cuenta");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
