﻿using ComercialHermanosCastro.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Domain.IServices
{
    public interface IVentaService
    {
        Task<VentaDto> GenerarVenta(VentaDto venta);
        Task<List<VentaDto>> Historial(string filtrarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<List<ReporteDto>> Reporte(string fechaInicio, string fechaFin);
        
    }
}
