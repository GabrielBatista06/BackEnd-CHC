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
    public class CuentasPendientesController : ControllerBase
    {
        private readonly ICuentaPendienteService _cuentaPendienteService;

        public CuentasPendientesController(ICuentaPendienteService cuentaPendienteService)
        {
            _cuentaPendienteService = cuentaPendienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ListaCuentasPendientes()
        {
            var result = new Response<List<CuentasPendientesDto>>();
            try
            {
                result.status = true;
                result.value = await _cuentaPendienteService.Lista();
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("cuentasPendientesAtrasadas")]
        public async Task<IActionResult> ListaCuentasPendientesAtrasadas()
        {
            var result = new Response<List<CuentasPendientesAtrasadasDto>>();
            try
            {
                result.status = true;
                result.value = await _cuentaPendienteService.ListaCuentasAtraso();
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("totalPendiente")]
        public async Task<IActionResult> Resumen()
        {
            var result = new Response<TotalPendienteGeneralDto>();
            try
            {
                result.status = true;
                result.value = await _cuentaPendienteService.Resumen();
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
