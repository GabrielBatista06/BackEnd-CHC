using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductoDto> Crear(ProductoDto productoDto)
        {
            return await _repository.Crear(productoDto);
        }

        public async Task<bool> Editar(ProductoDto productoDto)
        {
            return await _repository.Editar(productoDto);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _repository.Eliminar(id);

        }

        public async Task<List<ProductoDto>> Lista()
        {
            return await _repository.Lista();
        }
    }
}
