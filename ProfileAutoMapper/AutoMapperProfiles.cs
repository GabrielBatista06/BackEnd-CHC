using AutoMapper;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using System;
using System.Globalization;

namespace ComercialHermanosCastro.ProfileAutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, CrearClienteDto>().ReverseMap();       
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Pago, PagoDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
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
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre)).ForMember(destino =>
                destino.Comision,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Comision.Value, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                    destino.ApellidosCliente,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdClienteNavigation.Apellidos))
                ).ForMember(destino =>
                    destino.Apodo,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdClienteNavigation.Apodo))
                ) .ForMember(destino =>
                    destino.Cedula,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdClienteNavigation.Cedula))
                );

            CreateMap<VentaDto, Ventas>().ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => Convert.ToDateTime(origen.FechaRegistro))).ForMember(destino =>
                destino.Comision,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Comision, new CultureInfo("es-PE")))
                );

            #region DetalleVenta
            CreateMap<DetalleVentas, DetalleVentaDto>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )

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
                    destino.Nombre,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Nombre)
                ).ForMember(destino =>
                    destino.Apellidos,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Apellidos)
                ).ForMember(destino =>
                    destino.Apodo,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.Apodo)
                ).ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total, new CultureInfo("es-PE"))));

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
                ).ForMember(destino =>
                    destino.TotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                    destino.TipoPago,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.TipoPago))
                ).ForMember(destino =>
                    destino.TipoVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.TipoVenta))
                ).ForMember(destino =>
                    destino.Comision,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Comision))
                ).ForMember(destino =>
                    destino.Descuento,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Descuento))
                ).ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.IdClienteNavigation.Nombre))
                ).ForMember(destino =>
                    destino.ApellidosCliente,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.IdClienteNavigation.Apellidos))
                ).ForMember(destino =>
                    destino.Cedula,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.IdClienteNavigation.Cedula))
                ).ForMember(destino =>
                    destino.Apodo,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.IdClienteNavigation.Apodo))
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
