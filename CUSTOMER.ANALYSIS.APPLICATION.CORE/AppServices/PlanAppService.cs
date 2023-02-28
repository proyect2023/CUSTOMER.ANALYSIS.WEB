using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
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
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class PlanAppService : IPlanAppService
    {
        private readonly IPlanRepository _planRepository;

        public PlanAppService(IPlanRepository planRepository)
        {
            this._planRepository = planRepository;
        }

        public MethodResponseDto ConsultarPlanes()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _planRepository.GetPlanes();

                responseDto.Data = result.Select(Plan => new PlanViewModel(Plan)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarPlan(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                int Id = int.Parse(Utilities.Crypto.DescifrarId(ID));

                Plan Plan = _planRepository.Get(Id);

                responseDto.Data = new PlanViewModel(Plan);

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto CrearPlan(PlanViewModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Plan Plan = _planRepository.Get(model.Nombre);
                if (Plan != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PLAN_REGISTRADO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Plan = new Plan
                {
                    IdTipoPlan = model.IdTipoPlan,
                    Nombre = model.Nombre,
                    Precio = decimal.Parse(Utilities.Utilidades.DepuraStrConvertNum(model.Precio)),
                    Velocidad = model.Velocidad,
                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true
                };

                responseDto.Estado = _planRepository.AddPlan(Plan) > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarPlan(PlanViewModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                int Id = int.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Plan Plan = _planRepository.Get(Id);
                if (Plan == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PLAN_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Plan.Nombre != model.Nombre)
                {
                    var existe = _planRepository.Get(model.Nombre);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_PLAN_REGISTRADO;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                Plan.IdTipoPlan = model.IdTipoPlan;
                Plan.Nombre = model.Nombre;
                Plan.Precio = decimal.Parse(Utilities.Utilidades.DepuraStrConvertNum(model.Precio));
                Plan.Velocidad = model.Velocidad;

                Plan.Ip = model.Ip;
                Plan.UsuarioModificacion = model.Usuario;
                Plan.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                responseDto.Estado = _planRepository.UpdatePlan(Plan) > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EliminarPlan(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                int Id = int.Parse(Utilities.Crypto.DescifrarId(ID));
                Plan Plan = _planRepository.Get(Id);
                if (Plan == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PLAN_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Plan.Ip = Ip;
                Plan.UsuarioEliminacion = Usuario;
                Plan.FechaEliminacion = Utilities.Utilidades.GetHoraActual();
                Plan.Estado = false;

                responseDto.Estado = _planRepository.UpdatePlan(Plan) > 0;
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
