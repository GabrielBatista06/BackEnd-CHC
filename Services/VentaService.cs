﻿using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ComercialHermanosCastro.DTOs.PagosDiaContadoDto;

namespace ComercialHermanosCastro.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<VentaDto> GenerarVenta(VentaDto venta)
        {
            return await _ventaRepository.GenerarVenta(venta);
        }

        public async Task<List<VentaDto>> Historial(string filtrarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            return await _ventaRepository.Historial(filtrarPor, numeroVenta, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteDto>> Reporte(string fechaInicio, string fechaFin)
        {
            return await _ventaRepository.Reporte(fechaInicio, fechaFin);
        }

        public async Task<PagosDiaContadoDto> PagosDiaContado(string fechaInicio, string fechaFin)
        {
            return await _ventaRepository.PagosDiaContado(fechaInicio, fechaFin);
        }
    }
}
