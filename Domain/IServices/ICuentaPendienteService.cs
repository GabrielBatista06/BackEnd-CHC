using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface ICuentaPendienteService
    {
        Task<bool> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto);
        Task<List<CuentasPendientesDto>> Lista();
    }
}
