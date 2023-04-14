using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PrototipoHerasme.Models;

namespace PrototipoHerasme.Data
{
    public partial class DBHERASMEContext : DbContext
    {
        public DBHERASMEContext()
        {
        }

        public DBHERASMEContext(DbContextOptions<DBHERASMEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; } = null!;
        public virtual DbSet<EstadoFactura> EstadoFacturas { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolesPermiso> RolesPermisos { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<TurnoTemporal> TurnoTemporals { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__cliente__677F38F5B6140B22");

                entity.ToTable("cliente");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.ApellidoCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido_cliente");

                entity.Property(e => e.CedulaCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cedula_cliente");

                entity.Property(e => e.EmailCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email_cliente");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cliente");

                entity.Property(e => e.TelefonoCliente)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("telefono_cliente");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura)
                    .HasName("PK__detalle___F6BFE343429457CF");

                entity.ToTable("detalle_factura");

                entity.Property(e => e.IdDetalleFactura).HasColumnName("id_detalle_factura");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.Property(e => e.PrecioServicio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio_servicio");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK_detalle_factura_factura");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("FK_detalle_factura_servicio");
            });

            modelBuilder.Entity<EstadoFactura>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__estado_f__86989FB2FFB1A76B");

                entity.ToTable("estado_factura");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.NombreEstado)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("nombre_estado");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK__factura__6C08ED53BF438E64");

                entity.ToTable("factura");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.FechaFactura)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("fecha_factura");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Sucursal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sucursal");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_cliente");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_estado_factura");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_usuario");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__permiso__228F224F0530BC1D");

                entity.ToTable("permiso");

                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NombrePermiso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_permiso");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__rol__6ABCB5E06352E279");

                entity.ToTable("rol");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_rol");
            });

            modelBuilder.Entity<RolesPermiso>(entity =>
            {
                entity.ToTable("roles_permisos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roles_permisos_permiso");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roles_permisos_rol");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio)
                    .HasName("PK__servicio__6FD07FDC98108F78");

                entity.ToTable("servicio");

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.NombreServicio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_servicio");

                entity.Property(e => e.PrecioServicio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio_servicio");
            });

            modelBuilder.Entity<TurnoTemporal>(entity =>
            {
                entity.HasKey(e => e.IdTurno)
                    .HasName("PK__turno_te__C68E7397D7C699D3");

                entity.ToTable("turno_temporal");

                entity.Property(e => e.IdTurno)
                    .ValueGeneratedNever()
                    .HasColumnName("id_turno");

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.Property(e => e.StadoTurno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("stado_turno");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__4E3E04ADDEE6801B");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.ApellidoUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido_usuario");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.FechaRegistro)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imagen");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_usuario");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pwd");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_usuario_rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
