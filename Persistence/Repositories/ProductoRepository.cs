using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoRepository(IGenericRepository<Producto> productoRepository,
                                                         IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductoDto>> Lista()
        {
            try
            {
                var wrkqryProducto =  await _productoRepository.Consultar();

                return _mapper.Map<List<ProductoDto>>(wrkqryProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProductoDto> Crear(ProductoDto productoDto)
        {
            try
            {
                var crearProducto = await _productoRepository.Crear(_mapper.Map<Producto>(productoDto));

                if (crearProducto.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }

                return _mapper.Map<ProductoDto>(crearProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ProductoDto productoDto)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(productoDto);

                var productoEncontrado = await _productoRepository.Obtener(o =>
                o.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("No se pudo encontrar el producto para editar");
                }

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.Marca = productoModelo.Marca;
                productoEncontrado.Modelo = productoModelo.Modelo;
                productoEncontrado.Tamano = productoModelo.Tamano;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;
                productoEncontrado.FechaRegistro = productoEncontrado.FechaRegistro;
                productoEncontrado.FechaEdicion = DateTime.Now;

                bool result = await _productoRepository.Editar(productoEncontrado);

                if (!result)
                {
                    throw new TaskCanceledException("No se pudo editar el producto ");
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepository.Obtener(o => o.IdProducto == id);

                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("No se pudo encontrar el producto para eliminar");
                }

                bool  result = await _productoRepository.Eliminar(productoEncontrado);
                if (!result)
                {
                    throw new TaskCanceledException("No se pudo eliminar el producto ");
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
