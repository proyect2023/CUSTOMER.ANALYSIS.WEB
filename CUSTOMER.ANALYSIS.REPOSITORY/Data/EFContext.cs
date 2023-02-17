using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
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
            OnModelCreatingPartialSeguridad(modelBuilder);
            OnModelCreatingPartialCliente(modelBuilder);
            OnModelCreatingPartialUtilidades(modelBuilder);
            OnModelCreatingPartialAnalisis(modelBuilder);
        }

        partial void OnModelCreatingPartialSeguridad(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialCliente(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialUtilidades(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialAnalisis(ModelBuilder modelBuilder);
    }
}
