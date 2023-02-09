﻿using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IUtilidadRepository
    {
        void InicializarDb();
        Parametros GetParametro(string Codigo);
        List<Rol> GetRolesPrincipales();
        bool AddRol(int Id, string Nombre);
        void AgregarUsuarioAdministrador();
    }
}
    