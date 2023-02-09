
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUSTOMER.ANALYSIS.REPOSITORY.Data
{
    public partial class EFContext
    {
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolPermiso> RolPermiso { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
        public virtual DbSet<AccesoUsuario> AccesoUsuario { get; set; }
        
        partial void OnModelCreatingPartialSeguridad(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__SPPermis__0D626EC845308855");

                entity.ToTable("Permisos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Icono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NombreAbreviado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584CB35FDE7B");

                entity.ToTable("Rol");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.ToTable("RolPermiso");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.RolPermiso)
                    .HasForeignKey(d => d.IdPermiso)
                    .HasConstraintName("FK__RolPerm__IdPer__3F466844");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolPermiso)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__RolPerm__IdRol__3E52440B");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__SPUsuari__5B65BF97D476A28D");

                entity.ToTable("Usuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualizarPassword).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaUltimaConexion).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.ToTable("UsuarioRol");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__IdRol__3C69FB99");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Usuario__IdUsu__3B75D760");
            });

            modelBuilder.Entity<AccesoUsuario>(entity =>
            {
                entity.ToTable("AccesoUsuario");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SitioWeb)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });


        }

    }
}
