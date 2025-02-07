﻿using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IRepositories
{
    public interface ICuentaPendienteRepository
    {
        Task<int> GenerarCuentaPendiente(CuentasPendientesDto cuentasPendientesDto);
        Task<List<CuentasPendientesDto>> Lista();
        Task<List<CuentasPendientesAtrasadasDto>> ListaCuentasAtraso();
        Task<TotalPendienteGeneralDto> Resumen();
    }
}
