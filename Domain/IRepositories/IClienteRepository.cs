using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface IClienteRepository
    {
        Task<ClienteDto> CrearCliente(CrearClienteDto crearClienteDto);
         Task <List<ClienteDto>> GetCliente();
        Task< bool> EditarCliente(int id, ClienteDto clienteDto);
        Task<bool> ValidaCedula(string cedula);

    }
}
