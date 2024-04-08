using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface ICuentaPendienteRepository
    {
        Task<bool> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto);
        Task<List<CuentasPendientesDto>> Lista();
        Task<TotalPendienteGeneralDto> Resumen();
    }
}
