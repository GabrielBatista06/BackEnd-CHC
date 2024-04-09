using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<TotalPendienteGeneralDto> Resumen()
        {
            TotalPendienteGeneralDto pendienteGeneralDto = new TotalPendienteGeneralDto();
            pendienteGeneralDto.TotalPendienteGeneralCredito = await TotalIngresosGeneralCredito();

            return pendienteGeneralDto;
        }


        public async Task<string> TotalIngresosGeneralCredito()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<CuentasPendiente> _cuentasQuery = await _cuentaRepository.Consultar();

                if (_cuentasQuery.Count() > 0)
                {


                    resultado = _cuentasQuery
                         .Select(v => v.Total)
                         .Sum(v => v.Value);
                }


                return resultado.ToString("#,##0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw;
            }
        }
    }
}
