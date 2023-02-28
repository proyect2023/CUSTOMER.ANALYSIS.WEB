
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
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClientePlan> ClientePlans { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<TipoPlan> TipoPlans { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        partial void OnModelCreatingPartialCliente(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente).HasName("PK__Auditori__5EAF86A429612FD4");
                entity.ToTable("Cliente");
            });


            modelBuilder.Entity<ClientePlan>(entity =>
            {
                entity.HasKey(e => e.IdClientePlan).HasName("PK__ClienteP__B3CEC2F6CA4942C9");
                entity.ToTable("ClientePlan");

                entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClientePlans)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__ClientePl__IdCli__5165187F");

                entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.ClientePlans)
                    .HasForeignKey(d => d.IdPlan)
                    .HasConstraintName("FK__ClientePl__IdPla__5070F446");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.IdPlan).HasName("PK__Plan__FB8102AEBD14ECA3");
                entity.ToTable("Plan");

                entity.HasOne(d => d.IdTipoPlanNavigation).WithMany(p => p.Plans)
                    .HasForeignKey(d => d.IdTipoPlan)
                    .HasConstraintName("FK__Plan__IdTipoPlan__44FF419A");
            });

            modelBuilder.Entity<TipoPlan>(entity =>
            {
                entity.HasKey(e => e.IdTipoPlan).HasName("PK__TipoPlan__EED4109DB3C2DC2F");
                entity.ToTable("TipoPlan");

            });


        }

    }
}
