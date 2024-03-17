using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.IServices;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ComercialHermanosCastro.Persistence.DbContext;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class VentaRepository : IVentaRepository
    {

        private readonly IVentaGenericRepository _ventaGenericRepository;
        private readonly IGenericRepository<DetalleVentas> _detalleVentaRepository;
        private readonly IGenericRepository<Ventas> _ventaRepository;
        private readonly ICuentaPendienteService _cuentaPendiente;
        private readonly IMapper _mapper;
        private readonly AplicationDbContext _context;

        public VentaRepository(IVentaGenericRepository ventaGenericRepository,
                                                    IGenericRepository<DetalleVentas> detalleVentaRepository,
                                                    IGenericRepository<Ventas> ventaRepository,
                                                    IMapper mapper,
                                                    ICuentaPendienteService cuentaPendiente,
                                                    AplicationDbContext context)
        {
            _ventaGenericRepository = ventaGenericRepository;
            _detalleVentaRepository = detalleVentaRepository;
            _ventaRepository = ventaRepository;
            _mapper = mapper;
            _cuentaPendiente = cuentaPendiente;
            _context = context;
        }

        public async Task<VentaDto> GenerarVenta(VentaDto venta)
        {

            var usuario = _context.Usuarios.Where(u => u.Id == venta.Usuario).First();
            var cliente = _context.Clientes.Where(u => u.Id == venta.IdCliente).First();

            venta.FechaRegistro = DateTime.Now.ToString();

            CuentasPendientesDto cuentasPendientesDto = new CuentasPendientesDto();

            try
            {
                var ventaGenerada = await _ventaGenericRepository.Registrar(_mapper.Map<Ventas>(venta));

                if (ventaGenerada.IdVenta == 0)
                {
                    throw new TaskCanceledException("No se pudo generar la venta");
                }
                else if (venta.TipoVenta != "contado")
                {
                    cuentasPendientesDto.IdCliente = venta.IdCliente;
                    cuentasPendientesDto.Total = Convert.ToDecimal(venta.Total);
                    cuentasPendientesDto.DiaPago = venta.DiaPago;
                    cuentasPendientesDto.cuotas = venta.cuotas;
                    cuentasPendientesDto.valorCuota = cuentasPendientesDto.Total / venta.cuotas;
                    cuentasPendientesDto.fechaRegistro = DateTime.Now;
                    cuentasPendientesDto.numeroDocumento = ventaGenerada.NumeroDocumento;

                    await _cuentaPendiente.GenerarCuentaPendiente(cuentasPendientesDto);
                    PrintText(ventaGenerada, cliente, usuario, cuentasPendientesDto);

                    return _mapper.Map<VentaDto>(ventaGenerada);
                }

                PrintText(ventaGenerada, cliente, usuario, cuentasPendientesDto);
                return _mapper.Map<VentaDto>(ventaGenerada);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<List<VentaDto>> Historial(string filtrarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Ventas> query = await _ventaRepository.Consultar();
            var listResultado = new List<Ventas>();
            try
            {
                if (filtrarPor == "fecha")
                {
                    DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                    listResultado = await query.Where(o =>
                                                                                o.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                                                                               o.FechaRegistro.Value.Date <= fecha_fin.Date).
                                                                               Include(dv => dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation)
                                                                               .ToListAsync();
                }
                else
                {
                    listResultado = await query.Where(o => o.NumeroDocumento == numeroVenta).Include(dv =>
                                                                                    dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation)
                                                                                    .ToListAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<VentaDto>>(listResultado);
        }

        public async Task<List<ReporteDto>> Reporte(string fechaInicio, string fechaFin)
        {

            IQueryable<DetalleVentas> query = await _detalleVentaRepository.Consultar();
            var listResultado = new List<DetalleVentas>();
            try
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                listResultado = await query.
                    Include(p => p.IdProductoNavigation).
                    Include(v => v.IdVentaNavigation).
                    Where(o => o.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                                 o.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_fin.Date).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<ReporteDto>>(listResultado);
        }

        static void PrintText(Ventas venta, Cliente cliente, Usuario usuario, CuentasPendientesDto pendientesDto)
        {
            // Crear el objeto de impresión
            PrintDocument pd = new PrintDocument();

            // Suscribirse al evento PrintPage
            pd.PrintPage += (sender, e) => PrintPageEventHandler(sender, e, venta, cliente, usuario, pendientesDto);

            // Imprimir
            pd.Print();
        }

        private static void PrintPageEventHandler(object sender, PrintPageEventArgs e, Ventas venta, Cliente cliente, Usuario usuario, CuentasPendientesDto pendientesDto)
        {
          

            //Ruta Imagen
            string imagen = @"C:\Users\Angelo Santana\Desktop\Proyectos\Comercial Hermanos castro\FrontEnd\src\assets\img\Logo.jpeg";

            // Definir el contenido a imprimir
            Font font = new Font("Tahoma", 14);
            Font font_p = new Font("Tahoma", 8);
            Font font_p2 = new Font("Tahoma", 10);
            Font fontNormal = new Font("Tahoma", 10, FontStyle.Regular);
            int ancho = 1000;
            int y = 20;

            decimal? subTotal = venta.Total - venta.Comision;
            decimal? subTotalDescuento = venta.Total - venta.Descuento;
            string line = "------------------------------------------------------------------------";

            Image img = Image.FromFile(imagen);
            int alturaImagen = 100; // Ajusta la altura de la imagen 
            e.Graphics.DrawImage(img, new Rectangle(50, y, 180, alturaImagen));

            y += alturaImagen;

            if (venta.TipoVenta == "credito")
            {
                e.Graphics.DrawString("Comercial Hermanos Castro", font, Brushes.Blue, new RectangleF(25, y - 5, ancho, 20));
                e.Graphics.DrawString("C/P El Deán, M.P., al lado del parque central", font_p2, Brushes.Navy, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cel.: 829-940-4101 / 809-510-2849", font_p2, Brushes.Black, new RectangleF(25, y += 20, ancho, 20));
                e.Graphics.DrawString("Fecha: " + DateTime.Now, font_p2, Brushes.Black, new RectangleF(0, y += 40, ancho, 20));
                e.Graphics.DrawString("Factura #: " + venta.NumeroDocumento, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Atendido Por: " + usuario.NombreUsuario, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Venta A: " + venta.TipoVenta.Replace("contado", "Contado").Replace("credito", "Crédito"), font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cliente: " + cliente.Nombre + " " + cliente.Apellidos + $" ({cliente.Apodo})", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cel.: " + cliente.Celular + " Tel.: " + cliente.Telefono, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Día Pago Cuotas: Los días " + venta.DiaPago.ToString() + " de cada mes", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cant. Cuotas: " + pendientesDto.cuotas, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Valor Cuotas: " + pendientesDto.valorCuota?.ToString("0.00"), font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Producto".PadRight(13) + "Cant.".PadRight(10) + "Precio".PadRight(15) + "Total", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));


                foreach (var item in venta.DetalleVenta)
                {
                    e.Graphics.DrawString(item.IdProductoNavigation.Nombre, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                    e.Graphics.DrawString("                 " + item.Cantidad + espaciar(item.Cantidad.ToString().Length, 12) + item.Precio?.ToString("0.00") + espaciar(item.Precio.ToString().Length, 13) +
                    item.Total, fontNormal, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                }
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Sub Total RD$                " + subTotal?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));
                e.Graphics.DrawString("Financiamiento RD$        " + venta.Comision?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));
                e.Graphics.DrawString("Total RD$                      " + venta.Total?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));


                e.Graphics.DrawString("Servicio, calidad y eficiencia, todo en uno.", font_p2, Brushes.Black, new RectangleF(20, y += 80, ancho, 20));
                e.Graphics.DrawString("------- Gracias por elegirnos --------", font_p2, Brushes.Black, new RectangleF(38, y += 20, ancho, 20));
            }
            else
            {
                e.Graphics.DrawString("Comercial Hermanos Castro", font, Brushes.Blue, new RectangleF(25, y - 5, ancho, 20));
                e.Graphics.DrawString("C/P El Deán, M.P., al lado del parque central", font_p2, Brushes.Navy, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cel.: 829-940-4101 / 809-510-2849", font_p2, Brushes.Black, new RectangleF(25, y += 20, ancho, 20));
                e.Graphics.DrawString("Fecha: " + DateTime.Now, font_p2, Brushes.Black, new RectangleF(0, y += 40, ancho, 20));
                e.Graphics.DrawString("Factura #: " + venta.NumeroDocumento, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Atendido Por: " + usuario.NombreUsuario, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Venta A: " + venta.TipoVenta.Replace("contado", "Contado").Replace("credito", "Crédito"), font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cliente: " + cliente.Nombre + " " + cliente.Apellidos + $" ({cliente.Apodo})", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cel.: " + cliente.Celular + " Tel.: " + cliente.Telefono, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                //e.Graphics.DrawString("Día Pago Cuotas: Los días " + venta.DiaPago.ToString() + " de cada mes", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                //e.Graphics.DrawString("Cant. Cuotas: " + pendientesDto.cuotas, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                //e.Graphics.DrawString("Valor Cuotas: " + pendientesDto.valorCuota?.ToString("0.00"), font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Producto".PadRight(13) + "Cant.".PadRight(10) + "Precio".PadRight(15) + "Total", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));

                foreach (var item in venta.DetalleVenta)
                {
                    e.Graphics.DrawString(item.IdProductoNavigation.Nombre, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                    e.Graphics.DrawString("                 " + item.Cantidad + espaciar(item.Cantidad.ToString().Length, 12) + item.Precio?.ToString("0.00") + espaciar(item.Precio.ToString().Length, 13) +
                    item.Total, fontNormal, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                }
                e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Sub Total RD$                " + venta.Total?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));
                e.Graphics.DrawString("Descuento RD$              " + venta.Descuento?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));
                e.Graphics.DrawString("Total RD$                      " + subTotalDescuento?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));


                e.Graphics.DrawString("Servicio, calidad y eficiencia, todo en uno.", font_p2, Brushes.Black, new RectangleF(20, y += 80, ancho, 20));
                e.Graphics.DrawString("------- Gracias por elegirnos --------", font_p2, Brushes.Black, new RectangleF(38, y += 20, ancho, 20));
            }

        }

        public static string espaciar(int tamano, int valor)
        {
            string espacio = "";
            int subvalor = 0;

            subvalor = valor - tamano;
            for (int i = 0; i < subvalor; i++)
            {
                espacio = espacio + " ";
            }

            return espacio;
        }
    }
}
