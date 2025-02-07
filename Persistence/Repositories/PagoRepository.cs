﻿using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Persistence.DbContext;
using ComercialHermanosCastro.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class PagoRepository : IPagoRepository
    {

        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Pago> _pagoRepository;

        public PagoRepository(AplicationDbContext context, IMapper mapper, IGenericRepository<Pago> pagoRepository)
        {
            _context = context;
            _mapper = mapper;
            _pagoRepository = pagoRepository;
        }

        public async Task<bool> RealizarPago(PagoDto pagoDto)
        {

            int CantidadDigitos = 6;
            //usaremos transacion, ya que si ocurre un error en algun insert a una tabla, debe reestablecer todo a cero, como si no hubo o no existió ningun insert
            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    CuentasPendiente cuentasPendiente = _context.CuentasPendientes.Where(p => p.Id == pagoDto.IdcuentaPendiente).First();
                    decimal? montoAnterior = cuentasPendiente.Total;

                    cuentasPendiente.Total -= pagoDto.MontoPagado;
                    _context.CuentasPendientes.Update(cuentasPendiente);

                    await _context.SaveChangesAsync();

                    //Actualizar número de comprobante de pago
                    NumeroDocumentoPago correlativo = _context.NumeroDocumentosPagos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _context.NumeroDocumentosPagos.Update(correlativo);
                    await _context.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);
                    //Termina aquí lógica para generar y actualizar número comprobante de pago

                    //Creamos objeto para registrar el pago
                    Pago pago = new Pago
                    {
                        usuarioCobro = pagoDto.UsuarioCobro,
                        idcuentaPendiente = pagoDto.IdcuentaPendiente,
                        balanceAnterior = montoAnterior,
                        montoPagado = pagoDto.MontoPagado,
                        fechaPago = DateTime.Now,
                        tipoPago = pagoDto.TipoPago,
                        numeroDocumento = numeroVenta
                    };

                    await _context.Pagos.AddAsync(pago);
                    await _context.SaveChangesAsync();

                    var usuario = _context.Usuarios.Where(u => u.Id == pagoDto.UsuarioCobro).First();
                    var cliente = _context.Clientes.Where(u => u.Id == cuentasPendiente.IdCliente).First();

                    PrintText(cliente, usuario, cuentasPendiente, pagoDto, numeroVenta);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return true;
        }

        public async Task<DashBoardPagosSemanaDto> Resumen()
        {
            DashBoardPagosSemanaDto dashBoardDTO = new DashBoardPagosSemanaDto();
            try
            {
                dashBoardDTO.TotalPagos = await TotalPagosUltimaSemana();
                dashBoardDTO.TotalIngresos = await TotalIngresosUltimaSemana();


                List<PagosUltimaSemanaDto> listaPagosSemanaDtos = new List<PagosUltimaSemanaDto>();

                foreach (KeyValuePair<string, decimal?> item in await PagosUltimaSemana())
                {
                    listaPagosSemanaDtos.Add(new PagosUltimaSemanaDto()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                dashBoardDTO.PagosUltimaSemana = listaPagosSemanaDtos;

            }
            catch (Exception)
            {

                throw;
            }

            return dashBoardDTO;
        }
        public async Task<PagosMesDto> TotalIngresosPagos(string? anoActual)
        {
            if (anoActual == null)
            {
                anoActual = DateTime.Now.Year.ToString();
            }

            PagosMesDto pagosMesDto = new PagosMesDto();
            try
            {
                IQueryable<Pago> _pagoQuery = await _pagoRepository.Consultar();

                if (_pagoQuery.Count() > 0)
                {

                    // Utilizamos LINQ para agrupar los pagos por mes y calcular el total de pagos en cada mes
                    var pagosPorMes = _pagoQuery
                        .Where(c => c.fechaPago.Value.Year.ToString() == anoActual.ToString())
                        .GroupBy(p => p.fechaPago.Value.Month) // Agrupar por mes
                        .Select(g => new
                        {
                            Mes = g.Key,
                            TotalPagos = g.Sum(p => p.montoPagado)
                        });

                    List<PagosMes> listaPagosMesDtos = new List<PagosMes>();

                    foreach (var pagoMes in pagosPorMes)
                    {
                        listaPagosMesDtos.Add(new PagosMes()
                        {
                            Mes = Enum.GetName(typeof(EnumMesesDelAno), pagoMes.Mes),
                            Total = pagoMes.TotalPagos?.ToString()

                        });

                    }
                    pagosMesDto.PagosMesTotal = listaPagosMesDtos;
                    pagosMesDto.TotalCobrosGeneral = await TotalIngresosGeneralCobros();
                }


            }
            catch
            {
                throw;
            }
            return pagosMesDto;
        }

        public async Task<string> TotalIngresosGeneralCobros()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Pago> _cuentasQuery = await _pagoRepository.Consultar();

                if (_cuentasQuery.Count() > 0)
                {


                    resultado = _cuentasQuery
                         .Select(v => v.montoPagado)
                         .Sum(v => v.Value);
                }


                return resultado.ToString("#,##0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Pago> _pagoQuery = await _pagoRepository.Consultar();

                if (_pagoQuery.Count() > 0)
                {
                    var tablaPago = retornarVenta(_pagoQuery, -7);

                    resultado = tablaPago
                         .Select(v => v.montoPagado)
                         .Sum(v => v.Value);
                }


                return resultado.ToString("#,##0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw;
            }

        }

        public async Task<string> TotalPagosUltimaSemana()
        {
            int total = 0;
            try
            {
                IQueryable<Pago> _pagoQuery = await _pagoRepository.Consultar();

                if (_pagoQuery.Count() > 0)
                {

                    var tablaPago = retornarVenta(_pagoQuery, -7);
                    total = tablaPago.Count();
                }

                return total.ToString("#,##0.00", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, decimal?>> PagosUltimaSemana()
        {
            Dictionary<string, decimal?> resultado = new Dictionary<string, decimal?>();
            try
            {
                IQueryable<Pago> _ventaQuery = await _pagoRepository.Consultar();
                if (_ventaQuery.Count() > 0)
                {

                    var tablaVenta = retornarVenta(_ventaQuery, -7);

                    resultado = tablaVenta
                        .GroupBy(v => v.fechaPago.Value.Date).OrderBy(g => g.Key)
                        .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Sum(p => p.montoPagado) })
                        .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
                }

                return resultado;

            }
            catch
            {
                throw;
            }

        }


        private IQueryable<Pago> retornarVenta(IQueryable<Pago> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.fechaPago).Select(v => v.fechaPago).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.fechaPago.Value.Date >= ultimaFecha.Value.Date);
        }

        static void PrintText(Cliente cliente, Usuario usuario, CuentasPendiente cuentasPendientes, PagoDto pagoDto, string _numeroVenta)
        {
            // Crear el objeto de impresión
            PrintDocument pd = new PrintDocument();

            // Suscribirse al evento PrintPage
            pd.PrintPage += (sender, e) => PrintPageEventHandler(sender, e, cliente, usuario, cuentasPendientes, pagoDto, _numeroVenta);

            // Imprimir
            pd.Print();
        }

        private static void PrintPageEventHandler(object sender, PrintPageEventArgs e, Cliente cliente, Usuario usuario, CuentasPendiente cuentasPendientes, PagoDto pagoDto, string _numeroVenta)
        {
            //TODO

            //Ruta Imagen
            const string imagen = @"C:\Users\Angelo Santana\Desktop\carpeta con todo\Proyectos\Comercial Hermanos castro\FrontEnd\assets\img\Logo.jpeg";
            // Definir el contenido a imprimir
            Font font = new Font("Tahoma", 14);
            Font font_p = new Font("Tahoma", 8);
            Font font_p2 = new Font("Tahoma", 10);
            Font fontNormal = new Font("Tahoma", 10, FontStyle.Regular);
            int ancho = 1000;
            int y = 20;

            string line = "------------------------------------------------------------------------";

            Image img = Image.FromFile(imagen);
            int alturaImagen = 100; // Ajusta la altura de la imagen 
            e.Graphics.DrawImage(img, new Rectangle(50, y, 180, alturaImagen));

            y += alturaImagen;
            e.Graphics.DrawString("Comercial Hermanos Castro", font, Brushes.Blue, new RectangleF(25, y - 5, ancho, 20));
            e.Graphics.DrawString("C/P El Deán, M.P., al lado del parque central", font_p2, Brushes.Navy, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Cel.: 829-940-4101 / 809-510-2849", font_p2, Brushes.Black, new RectangleF(25, y += 20, ancho, 20));
            e.Graphics.DrawString("Fecha: " + DateTime.Now, font_p2, Brushes.Black, new RectangleF(0, y += 40, ancho, 20));
            e.Graphics.DrawString("Atendido Por: " + usuario.NombreUsuario, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Núm. Comprobante Pago: " + _numeroVenta, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Cliente: " + cliente.Nombre + " " + cliente.Apellidos + $" ({cliente.Apodo})", font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Cel.: " + cliente.Celular + " Tel.: " + cliente.Telefono, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Núm. Factura Pendiente: " + cuentasPendientes.numeroDocumento, font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("Tipo De Pago: " + pagoDto.TipoPago.Replace("tarjeta", "Tarjeta").Replace("transferencia", "Transferencia").Replace("efectivo", "Efectivo"), font_p2, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));

            e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
            e.Graphics.DrawString("***Comprobante De Pago***", font_p2, Brushes.Black, new RectangleF(50, y += 20, ancho, 20));
            e.Graphics.DrawString(line, font_p, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));

            e.Graphics.DrawString("Balance Pendiente RD$         " + pagoDto.BalanceAnterior?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(20, y += 20, ancho, 20));
            e.Graphics.DrawString("Monto Pagado RD$               " + pagoDto.MontoPagado?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(20, y += 20, ancho, 20));
            e.Graphics.DrawString("Nuevo Bal. Pendiente RD$     " + cuentasPendientes.Total?.ToString("0.00").PadRight(0), font_p2, Brushes.Black, new RectangleF(20, y += 20, ancho, 20));

            //Footer de la factura
            e.Graphics.DrawString("Servicio, calidad y eficiencia, todo en uno.", font_p2, Brushes.Black, new RectangleF(20, y += 80, ancho, 20));
            e.Graphics.DrawString("------- Gracias por elegirnos --------", font_p2, Brushes.Black, new RectangleF(38, y += 20, ancho, 20));

        }
    }


}
