using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Services;
using ComercialHermanosCastro.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ComercialHermanosCastro.DTOs.PagosDiaContadoDto;

namespace ComercialHermanosCastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        private readonly IVentaService _ventaService;

        public PagoController(IPagoService pagoService, IVentaService ventaService)
        {
            _pagoService = pagoService;
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarPago([FromBody] PagoDto pagoDto)
        {
            var result = new Response<PagoDto>();

            try
            {
                result.status = true;
                result.value = null;
                await _pagoService.RealizarPago(pagoDto);
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Resumen(string? anoActual)
        {
            var result = new Response<PagosMesDto>();
            try
            {
                result.status = true;
                result.value = await _pagoService.TotalIngresosPagos(anoActual);
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
        public async Task<IActionResult> ResumenPagos()
        {
            var result = new Response<DashBoardPagosSemanaDto>();
            try
            {
                result.status = true;
                result.value = await _pagoService.Resumen();
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("pagosContados")]
        public async Task<IActionResult> PagosContados(string? fechaInicio, string? fechaFin)
        {
            var result = new Response<PagosDiaContadoDto>();
            try
            {
                result.status = true;
                result.value = await _ventaService.PagosDiaContado(fechaInicio, fechaFin);
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
