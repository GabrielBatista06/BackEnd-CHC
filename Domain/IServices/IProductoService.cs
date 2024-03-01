using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> Lista();
        Task<ProductoDto> Crear(ProductoDto productoDto);
        Task<bool> Editar(ProductoDto productoDto);
        Task<bool> Eliminar(int id);
    }
}
