using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface ICuentaPendienteService
    {
        Task<bool> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto);
    }
}
