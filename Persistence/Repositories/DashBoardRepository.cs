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
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly IVentaGenericRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public DashBoardRepository(IVentaGenericRepository ventaRepository,
                                                            IGenericRepository<Producto> productoRepository,
                                                            IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO dashBoardDTO = new DashBoardDTO();
            try
            {
                dashBoardDTO.TotalIngresosGeneral = await TotalIngresosGeneral();
                dashBoardDTO.TotalIngresosGeneralContado = await TotalIngresosGeneralContado();
                dashBoardDTO.TotalIngresosGeneralCredito = await TotalIngresosGeneralCredito();
                dashBoardDTO.TotalVentas = await TotalVentasUltimaSemana();
                dashBoardDTO.TotalIngresos = await TotalIngresosUltimaSemana();
                dashBoardDTO.TotalProductos = await TotalProductos();
                

                List<VentasSemanaDto> listaVentasSemanaDtos = new List<VentasSemanaDto>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentasSemanaDtos.Add(new VentasSemanaDto()
                    {
                        Fecha = item.Key,
                        Total = item.Value,
                    });
                }

                dashBoardDTO.VentasUltimaSemana = listaVentasSemanaDtos;

            }
            catch (Exception)
            {

                throw;
            }

            return dashBoardDTO;
        }

        private IQueryable<Ventas> retornarVenta(IQueryable<Ventas> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        public async Task<string> TotalVentasUltimaSemana()
        {
            int total = 0;
            try
            {
                IQueryable<Ventas> _ventaQuery = await _ventaRepository.Consultar();

                if (_ventaQuery.Count() > 0)
                {

                    var tablaVenta = retornarVenta(_ventaQuery, -7);
                    total = tablaVenta.Count();
                }

                return total.ToString("#,##0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Ventas> _ventaQuery = await _ventaRepository.Consultar();

                if (_ventaQuery.Count() > 0)
                {
                    var tablaVenta = retornarVenta(_ventaQuery, -7);

                    resultado = tablaVenta
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

        public async Task<string> TotalProductos()
        {
            try
            {
                IQueryable<Producto> query = await _productoRepository.Consultar();
                int total = query.Count();
                return total.ToString("#,##0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            try
            {
                IQueryable<Ventas> _ventaQuery = await _ventaRepository.Consultar();
                if (_ventaQuery.Count() > 0)
                {

                    var tablaVenta = retornarVenta(_ventaQuery, -7);

                    resultado = tablaVenta
                        .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                        .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                        .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
                }


                return resultado;

            }
            catch
            {
                throw;
            }

        }

        public async Task<string> TotalIngresosGeneral()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Ventas> _ventaQuery = await _ventaRepository.Consultar();

                if (_ventaQuery.Count() > 0)
                {


                    resultado = _ventaQuery
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
        public async Task<string> TotalIngresosGeneralContado()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Ventas> _ventaQuery = await _ventaRepository.Consultar();

                if (_ventaQuery.Count() > 0)
                {


                    resultado = _ventaQuery.Where(c => c.TipoVenta == "contado")
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

        public async Task<string> TotalIngresosGeneralCredito()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Ventas> _ventaQuery = await _ventaRepository.Consultar();

                if (_ventaQuery.Count() > 0)
                {


                    resultado = _ventaQuery.Where(c => c.TipoVenta == "credito")
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
