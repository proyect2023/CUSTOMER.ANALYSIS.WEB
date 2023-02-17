
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

        partial void OnModelCreatingPartialCliente(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente).HasName("PK__Auditori__5EAF86A429612FD4");
                entity.ToTable("Cliente");
            });

        }

    }
}
