using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.REPOSITORY.Data
{
    public partial class EFContext : DbContext
    {
        public string DefaultConecctionString = string.Empty;
        public virtual DbSet<Cliente> Cliente { get; set; }

        public EFContext() : base()
        {
            DefaultConecctionString = GlobalSettings.ConnectionString;
        }

        public EFContext(string connectionString)
              : base()
        {
            this.DefaultConecctionString = connectionString;
        }

        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DefaultConecctionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente).HasName("PK__Auditori__5EAF86A429612FD4");
                entity.ToTable("Cliente");
            });
        }
    }
}
