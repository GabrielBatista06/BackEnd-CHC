using ComercialHermanosCastro.DTOs;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface ICuentaPendienteRepository
    {
        Task<bool> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto);
    }
}
