using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IClienteRepository 
    {
        List<Cliente> GetAll();
    }
}
