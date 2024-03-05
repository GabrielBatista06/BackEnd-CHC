using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
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
    public class VentaRepository : IVentaRepository
    {
        private readonly IVentaGenericRepository _ventaGenericRepository;
        private readonly IGenericRepository<DetalleVentas> _detalleVentaRepository;
        private readonly IGenericRepository<Ventas> _ventaRepository;
        private readonly ICuentaPendienteService _cuentaPendiente;
        private readonly IMapper _mapper;

        public VentaRepository(IVentaGenericRepository ventaGenericRepository,
                                                    IGenericRepository<DetalleVentas> detalleVentaRepository,
                                                    IGenericRepository<Ventas> ventaRepository,
                                                    IMapper mapper,
                                                    ICuentaPendienteService cuentaPendiente)
        {
            _ventaGenericRepository = ventaGenericRepository;
            _detalleVentaRepository = detalleVentaRepository;
            _ventaRepository = ventaRepository;
            _mapper = mapper;
            _cuentaPendiente = cuentaPendiente;
        }

        public async Task<VentaDto> GenerarVenta(VentaDto venta)
        {
            venta.FechaRegistro = DateTime.Now.ToString("dd/MM/yyyy");

            CuentasPendientesDto cuentasPendientesDto = new CuentasPendientesDto();

            cuentasPendientesDto.IdCliente = venta.IdCliente;
            cuentasPendientesDto.Total = Convert.ToDecimal(venta.Total);
            cuentasPendientesDto.DiaPago = venta.DiaPago;
            try
            {
                var ventaGenerada = await _ventaGenericRepository.Registrar(_mapper.Map<Ventas>(venta));

                if (ventaGenerada.IdVenta == 0)
                {
                    throw new TaskCanceledException("No se pudo generar la venta");
                }
                await _cuentaPendiente.GenerarCuentaPendiente(cuentasPendientesDto);
                return _mapper.Map<VentaDto>(ventaGenerada);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<VentaDto>> Historial(string filtrarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Ventas> query = await _ventaRepository.Consultar();
            var listResultado = new List<Ventas>();
            try
            {
                if (filtrarPor == "fecha")
                {
                    DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                    listResultado = await query.Where(o =>
                                                                                o.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                                                                               o.FechaRegistro.Value.Date <= fecha_fin.Date).
                                                                               Include(dv =>dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation)
                                                                               .ToListAsync();
                }
                else
                {
                    listResultado = await query.Where(o => o.NumeroDocumento == numeroVenta).Include(dv =>
                                                                                    dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation)
                                                                                    .ToListAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<VentaDto>>(listResultado);
        }

        public async Task<List<ReporteDto>> Reporte(string fechaInicio, string fechaFin)
        {

            IQueryable<DetalleVentas> query = await _detalleVentaRepository.Consultar();
            var listResultado = new List<DetalleVentas>();
            try
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                listResultado = await query.
                    Include(p => p.IdProductoNavigation).
                    Include(v => v.IdVentaNavigation).
                    Where(o => o.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                                 o.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_fin.Date).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<ReporteDto>>(listResultado);
        }
    }
}
