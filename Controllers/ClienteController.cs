using ComercialHermanosCastro.Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using ComercialHermanosCastro.Services;
using ComercialHermanosCastro.Utils;

namespace ComercialHermanosCastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> Get()
        {
            var result = new Response<List<ClienteDto>>();
            try
            {
                result.status = true;
                result.value = await _clienteService.GetCliente();
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> crearCliente([FromBody] CrearClienteDto cliente)
        {
            try
            {

                await _clienteService.CrearCliente(cliente);
                return Ok(new { message = "Cliente registrado de manera exitosa" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteDto clienteDto)
        {
            var result = await  _clienteService.EditarCliente(id, clienteDto); 

            if (result == false)
            {
                return NotFound();
            }

            return Ok(new { message = "Cliente actualizado con éxito" });
        }
    }
}
