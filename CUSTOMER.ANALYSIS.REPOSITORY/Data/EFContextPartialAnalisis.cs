
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
        private DbSet<ConsultarTotalesDto> ConsultarTotalesDto { get; set; }
        partial void OnModelCreatingPartialAnalisis(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<ConsultarTotalesDto>().HasNoKey().ToView(null);
        }

    }
}
