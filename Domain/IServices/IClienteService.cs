using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface IClienteService
    {
        Task<ClienteDto> CrearCliente(CrearClienteDto crearClienteDto);
        Task<List<ClienteDto>> GetCliente();
        Task <bool> EditarCliente(int id, ClienteDto clienteDto);
    }
}
