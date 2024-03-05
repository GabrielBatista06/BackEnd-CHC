using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ComercialHermanosCastro.Domain.IServices;

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
        public async Task<IActionResult> ListaProducto()
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
    }
}
