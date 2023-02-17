
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
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
        public virtual DbSet<Notificacion> Notificaciones { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Correo> Correos { get; set; }
        private DbSet<RolesQueryDto> RolesQueryDto { get; set; }

        partial void OnModelCreatingPartialUtilidades(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<RolesQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<Correo>(entity =>
            {
                entity.HasKey(e => e.IdCorreo).HasName("PK__Correo__872F8EAEDC7FA84E");
                entity.ToTable("Correo");
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion).HasName("PK_EVNotificacion");
                entity.ToTable("Notificaciones");
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.HasKey(e => e.Codigo);
                entity.ToTable("Parametros");
            });

        }

    }
}
