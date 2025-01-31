using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClienteRepository(AplicationDbContext context,
                                                         IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task <ClienteDto>CrearCliente(CrearClienteDto crearClienteDto)
        {
            crearClienteDto.Activo = true;
            crearClienteDto.FechaCreacion = DateTime.Now;
            crearClienteDto.FechaEdicion = crearClienteDto.FechaCreacion;
            var result = _mapper.Map<Cliente>(crearClienteDto);

            _context.Add(result);

            await _context.SaveChangesAsync();

            return _mapper.Map<ClienteDto>(result);
        }

        public async Task<bool> EditarCliente(int id, ClienteDto clienteDto)
        {

            var result = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (clienteDto.Activo == false)
            {
                clienteDto.Id = id;
                clienteDto.Activo = false;
                clienteDto.FechaEdicion = DateTime.Now;

                _mapper.Map(clienteDto, result);
                _context.Entry(result).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            if (result == null)
            {
                return false;
            }
            clienteDto.Id = id;
            _mapper.Map(clienteDto, result);
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<List<ClienteDto>> GetCliente()
        {
            var clientes = await _context.Clientes.ToListAsync();

            return _mapper.Map<List<ClienteDto>>(clientes).OrderBy(O =>O.Nombre).ToList();
        }

        public async Task <bool> ValidaCedula(string cedula)
        {
            // Base URL del servicio
            string baseUrl = "https://api.digital.gob.do/v3/cedulas/";
            // Construir la URL completa
            string url = $"{baseUrl}{cedula}/validate";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Enviar solicitud GET
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como texto
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

    }
}
