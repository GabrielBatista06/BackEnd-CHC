using AutoMapper;
using ComercialHermanosCastro.Domain.IRepositories;
using ComercialHermanosCastro.Domain.Models;
using ComercialHermanosCastro.DTOs;
using ComercialHermanosCastro.Persistence.DbContext;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace ComercialHermanosCastro.Persistence.Repositories
{
    public class PagoRepository : IPagoRepository
    {

        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public PagoRepository(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> RealizarPago(PagoDto pagoDto)
        {
            //usaremos transacion, ya que si ocurre un error en algun insert a una tabla, debe reestablecer todo a cero, como si no hubo o no existió ningun insert
            using (var transaction = _context.Database.BeginTransaction())
            {
                
                try
                {

                    CuentasPendiente cuentasPendiente = _context.CuentasPendientes.Where(p => p.Id == pagoDto.IdcuentaPendiente).First();
                     decimal? montoAnterior = cuentasPendiente.Total;

                    cuentasPendiente.Total = cuentasPendiente.Total - pagoDto.MontoPagado;
                    _context.CuentasPendientes.Update(cuentasPendiente);

                    await _context.SaveChangesAsync();

                    Pago pago = new Pago
                    {
                        usuarioCobro = pagoDto.UsuarioCobro,
                        idcuentaPendiente = pagoDto.IdcuentaPendiente,
                        balanceAnterior = montoAnterior,
                        montoPagado = pagoDto.MontoPagado,
                        fechaPago = DateTime.Now,
                    };

                    await _context.Pagos.AddAsync(pago);
                    await _context.SaveChangesAsync();

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
    }
}
