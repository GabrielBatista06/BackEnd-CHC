using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface IClienteRepository
    {
        Task CrearCliente(CrearClienteDto crearClienteDto);
         Task <List<ClienteDto>> GetCliente();
        Task< bool> EditarCliente(int id, ClienteDto clienteDto);

    }
}
