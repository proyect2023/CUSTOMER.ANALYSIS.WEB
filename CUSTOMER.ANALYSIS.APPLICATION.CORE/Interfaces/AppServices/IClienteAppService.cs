using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IClienteAppService
    {
        MethodResponseDto ConsultarClientes();
        MethodResponseDto ConsultarCliente(string ID);
        MethodResponseDto CrearCliente(ClienteModel model);
        MethodResponseDto EditarCliente(ClienteModel model);
        MethodResponseDto EliminarCliente(string ID, string Ip, long Usuario);
    }
}
