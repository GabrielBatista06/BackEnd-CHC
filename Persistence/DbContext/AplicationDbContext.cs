using ComercialHermanosCastro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComercialHermanosCastro.Persistence.DbContext
{
    public partial class AplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> dbContext)
: base(dbContext)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CuentasPendiente> CuentasPendientes { get; set; }
        public DbSet<DetalleVentas> DetalleVenta { get; set; }
        public DbSet<NumeroDocumento> NumeroDocumentos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Ventas> Venta { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__07F4A1323E322568");

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.Stock).HasColumnName("stock");

            });

            modelBuilder.Entity<DetalleVentas>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DetalleV__BFE2843FA3FFCC43");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleVe__idPro__267ABA7A");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__idVen__25869641");
            });

            modelBuilder.Entity<Ventas>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__077D561409C275F1");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.Property(e => e.Comision)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("comision");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__DetalleVe__idVen__25869647");

                entity.HasOne(d => d.IdClienteNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__DetalleVe__idVen__25869645");


            });

            modelBuilder.Entity<CuentasPendiente>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Venta__077D561409C275F2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiaPago)
                    .HasColumnType("int")
                    .HasColumnName("diaPago");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Venta__idCliente__3A4CA8FD");

            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Venta__077D561409C275F1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.balanceAnterior)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("balanceAnterior");

                entity.Property(e => e.montoPagado)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("montoPagado");

                entity.Property(e => e.fechaPago)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaPago")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UsuarioCobroNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.usuarioCobro)
                    .HasConstraintName("FK__Pagos__usuarioCo__14270015");

                entity.HasOne(d => d.IdcuentaPendienteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.idcuentaPendiente)
                    .HasConstraintName("FK__Pagos__idcuentaP__151B244E");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
