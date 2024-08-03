using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface ICuentaPendienteService
    {
        Task<int> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto);
        Task<List<CuentasPendientesDto>> Lista();
        Task<List<CuentasPendientesAtrasadasDto>> ListaCuentasAtraso();
        Task<TotalPendienteGeneralDto> Resumen();
    }
}
