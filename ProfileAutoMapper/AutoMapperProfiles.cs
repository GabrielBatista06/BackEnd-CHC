using AutoMapper;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System.Globalization;
using System;

namespace ComercialHermanosCastro.ProfileAutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, CrearClienteDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Pago, PagoDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-PE")))
            ).ForMember(destino =>
            destino.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<ProductoDto, Producto>().ForMember(destino =>
               destino.Precio,
               opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
           ).ForMember(destino =>
           destino.EsActivo,
           opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));


            CreateMap<Ventas, VentaDto>().ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy")))
                .ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre));

            CreateMap<VentaDto, Ventas>().ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaRegistro))).ForMember(destino =>
                destino.FechaPago,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaPago))); 

            #region DetalleVenta
            CreateMap<DetalleVentas, DetalleVentaDto>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                
                //.ForMember(destino =>
                //    destino.NombreCliente,
                //    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre)
                //)
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );

            CreateMap<DetalleVentaDto, DetalleVentas>()
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
                );
            #endregion

            #region Cuenta Pendiente
            CreateMap<CuentasPendiente, CuentasPendientesDto>()
                .ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre)
                ).ForMember(destino =>
                    destino.ApellidoCliente,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Apellidos)
                ).ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                    destino.FechaPago,
                    opt => opt.MapFrom(origen => origen.FechaPago.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<CuentasPendientesDto, CuentasPendiente>()
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
                );
            #endregion

            #region Reporte
            CreateMap<DetalleVentas, ReporteDto>()
                .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.NumeroDocumento,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                .ForMember(destino =>
                    destino.TipoPago,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                )
                .ForMember(destino =>
                    destino.TotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Producto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total, new CultureInfo("es-PE")))
                );
            #endregion Reporte


        }

    }
}
