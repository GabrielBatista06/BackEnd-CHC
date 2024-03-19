using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<CuentasPendientesDto>> Lista()
        {
            try
            {
                var wrkqryProducto = await _cuentaRepository.Consultar();

                var listaCuentasPendientes = wrkqryProducto.Include(c => c.IdClienteNavigation);
                int diaActual = DateTime.Now.Day;

                var result = _mapper.Map<List<CuentasPendientesDto>>(listaCuentasPendientes.ToList().Where(q => q.Total > 0)).OrderBy(o => o.DiaPago == diaActual ? 0 : 1).ThenBy(o => o.DiaPago).ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
