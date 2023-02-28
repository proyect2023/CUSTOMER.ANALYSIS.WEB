using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAppService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public MethodResponseDto ConsultarClientes()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _clienteRepository.GetClientes();

                responseDto.Data = result.Select(cliente => new ClienteModel(cliente)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarCliente(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                int Id = int.Parse(Utilities.Crypto.DescifrarId(ID));

                Cliente cliente = _clienteRepository.Get(Id);

                responseDto.Data = new ClienteModel(cliente);

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto CrearCliente(ClienteModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Cliente cliente = _clienteRepository.GetFirstOrDefault(x => x.Identificacion == model.Identificacion && x.Estado == true);
                if (cliente != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_REGISTRADO_IDENTIFICACION;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                cliente = new Cliente
                {
                    TipoPersona = model.TipoPersona,
                    TipoIdentificacion = model.TipoIdentificacion,
                    Identificacion = model.Identificacion,
                    RazonSocial = model.RazonSocial,
                    Direccion = model.Direccion,
                    CorreoElectronico = model.CorreoElectronico,
                    Telefono = model.Telefono,
                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true
                };

                _clienteRepository.Add(cliente);
                responseDto.Estado = _clienteRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarCliente(ClienteModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                int Id = int.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Cliente cliente = _clienteRepository.Get(Id);
                if (cliente == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (cliente.Identificacion != model.Identificacion)
                {
                    var existe = _clienteRepository.GetFirstOrDefault(x => x.Identificacion == model.Identificacion && x.Estado == true && x.IdCliente != Id);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_REGISTRADO_IDENTIFICACION;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                cliente.TipoPersona = model.TipoPersona;
                cliente.TipoIdentificacion = model.TipoIdentificacion;
                cliente.Identificacion = model.Identificacion;
                cliente.RazonSocial = model.RazonSocial;
                cliente.Direccion = model.Direccion;
                cliente.CorreoElectronico = model.CorreoElectronico;
                cliente.Telefono = model.Telefono;
                cliente.Ip = model.Ip;
                cliente.UsuarioModificacion = model.Usuario;
                cliente.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _clienteRepository.Update(cliente);
                responseDto.Estado = _clienteRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EliminarCliente(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                int Id = int.Parse(Utilities.Crypto.DescifrarId(ID));
                Cliente cliente = _clienteRepository.Get(Id);
                if (cliente == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                cliente.Ip = Ip;
                cliente.UsuarioEliminacion = Usuario;
                cliente.FechaEliminacion = Utilities.Utilidades.GetHoraActual();
                cliente.Estado = false;

                _clienteRepository.Update(cliente);
                responseDto.Estado = _clienteRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }
    }
}
