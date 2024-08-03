using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ClienteDto> CrearCliente(CrearClienteDto crearClienteDto)
        {
          return  await _clienteRepository.CrearCliente(crearClienteDto);
        }

        public async Task<bool> EditarCliente(int id, ClienteDto clienteDto)
        {
            return await _clienteRepository.EditarCliente(id, clienteDto);
        }

        public async Task<List<ClienteDto>> GetCliente()
        {
            return await _clienteRepository.GetCliente();
        }

    }
}
