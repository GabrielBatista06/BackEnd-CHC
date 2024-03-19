using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
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
    }
}
