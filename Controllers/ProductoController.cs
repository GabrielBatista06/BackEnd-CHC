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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task <IActionResult> ListaProducto()
        {
            var result = new Response<List<ProductoDto>>();
            try
            {
                result.status = true;
                result.value = await _productoService.Lista();
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoDto productoDto)
        {
          
            var result = new Response<ProductoDto>();

            try
            {
                result.status = true;
                result.value = await _productoService.Crear(productoDto);
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] ProductoDto productoDto)
        {
            var result = new Response<bool>();

            try
            {
                result.status = true;
                result.value = await _productoService.Editar(productoDto);
            }
            catch (Exception ex)
            {

                result.status = false;
                result.msg = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = new Response<bool>();

            try
            {
                result.status = true;
                result.value = await _productoService.Eliminar(id);
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
