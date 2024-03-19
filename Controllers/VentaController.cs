using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VentaDto ventaDto)
        {
            var result = new Response<VentaDto>();
            try
            {
                result.status = true;
                result.value = await _ventaService.GenerarVenta(ventaDto);
            }
            catch (Exception ex)
            {
                result.status = false;
                result.msg = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListaVentas(string filtrarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var result = new Response<List<VentaDto>>();

            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                result.status = true;
                result.value = await _ventaService.Historial(filtrarPor, numeroVenta, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var result = new Response<List<ReporteDto>>();

            try
            {
                result.status = true;
                result.value = await _ventaService.Reporte(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);
        }
    }
}
