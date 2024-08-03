using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Services;
using ComercialHermanosCastro.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            //try
            //{

            //    await _clienteService.CrearCliente(cliente);
            //    return Ok(new { message = "Cliente registrado de manera exitosa" });
            //}
            //catch (Exception ex)
            //{

            //    return BadRequest(ex.Message);
            //}
            var result = new Response<ClienteDto>();

            try
            {
                result.status = true;
                result.value = await _clienteService.CrearCliente(cliente);
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteDto clienteDto)
        {
            //    var result = await  _clienteService.EditarCliente(id, clienteDto); 

            //    if (result == false)
            //    {
            //        return NotFound();
            //    }

            //    return Ok(new { message = "Cliente actualizado con éxito" });
            //}

            var result = new Response<bool>();

            try
            {
                result.status = true;
                result.value = await _clienteService.EditarCliente(id, clienteDto);
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

