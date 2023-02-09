using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities.StoreProcedures;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Extensions;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class RolAppService : BaseAppService, IRolAppService
    {
        private readonly IRolQueryService _rolQueryService;
        private readonly IRolRepository _rolRepository;
        private readonly IPortalQueryService _portalQueryService;

        public RolAppService(IRolQueryService rolQueryService,
            IRolRepository rolRepository,
            IPortalQueryService portalQueryService)
        {
            _rolQueryService = rolQueryService;
            _rolRepository = rolRepository;
            _portalQueryService = portalQueryService;
        }

        public (List<RolAppResultDto>, string) ConsultarRoles(long idCompania)
        {
            List<RolAppResultDto> rolesAppDto = null;
            string mensaje = null;
            try
            {
                var roles = _rolRepository.GetRoles().Select(c => new RolesQueryDto { Estado = c.Estado ?? false, IdRol = c.IdRol, Nombre = c.Nombre });

                if (string.IsNullOrEmpty(mensaje))
                    rolesAppDto = roles.Select(fc => fc.MapToRolesAppResultDto()).ToList();
                else
                    mensaje = string.Format("{0} => {1}", this.GetCaller(), mensaje);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
            }

            return (rolesAppDto, mensaje);
        }

        public (bool, string) RegistrarRol(long IdUsuario, string nombre, bool estado)
        {
            string mensaje = null;
            try
            {
                var result = _rolRepository.RegistrarRol(new SPRol { IdUsuario = IdUsuario, Nombre = nombre, Estado = estado }, ref mensaje);

                if (!result)
                    mensaje = string.Format("{0} => {1}", this.GetCaller(), mensaje);

                return (result, null);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return (false, mensaje);
            }
        }

        public (bool, string) ActualizarRol(long idrol, string nombre, bool estado, long IdUsuario)
        {
            string mensaje = null;
            try
            {
                var result = _rolRepository.ActualizaRol(new SPRol { IdRol = idrol, Nombre = nombre, Estado = estado}, ref mensaje);

                if (!result)
                    mensaje = string.Format("{0} => {1}", this.GetCaller(), mensaje);

                return (result, null);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return (false, mensaje);
            }
        }

        public (List<IdQueryDto>, string) ConsultarRolVentanas(short IdRol, string UsuarioAuditoria)
        {
            string mensaje = null;
            try
            {
                var result = _rolQueryService.ConsultarRolVentanas(IdRol, ref mensaje);
                if (result == null) throw new Exception(mensaje);
                return (result, null);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return (null, mensaje);
            }
        }

        public (List<AsignarRolVentanaAppResultDto>, string) ConsultarVentanasActivas(string UsuarioAuditoria)
        {
            string mensaje = null;
            try
            {
                var result = _portalQueryService.ConsultarVentanasActivas(ref mensaje);
                if (result == null) throw new Exception(mensaje);
                var items = new List<AsignarRolVentanaAppResultDto>();
                var result1 = result.Where(x => x.IdPadre == null);
                if (result1 != null)
                {
                    foreach (var i in result1)
                    {
                        var item1 = new AsignarRolVentanaAppResultDto() { IdPermiso = i.IdPermiso, Nombre = i.NombreAbreviado };
                        var result2 = result.Where(x => x.IdPadre == i.IdPermiso);
                        foreach (var j in result2)
                        {
                            var item2 = new AsignarRolVentanaAppResultDto() { IdPermiso = j.IdPermiso, Nombre = j.NombreAbreviado };
                            var result3 = result.Where(x => x.IdPadre == j.IdPermiso);
                            foreach (var k in result3)
                            {
                                var item3 = new AsignarRolVentanaAppResultDto() { IdPermiso = k.IdPermiso, Nombre = k.NombreAbreviado };
                                var result4 = result.Where(x => x.IdPadre == k.IdPermiso);
                                foreach (var l in result4)
                                    item3.Items.Add(new AsignarRolVentanaAppResultDto() { IdPermiso = l.IdPermiso, Nombre = l.NombreAbreviado });
                                item2.Items.Add(item3);
                            }
                            item1.Items.Add(item2);
                        }
                        items.Add(item1);
                    }
                }
                return (items, null);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return (null, mensaje);
            }
        }

        public (int?, string) Asignar(long idRol, string idPermisos, long usuarioAuditoria)
        {
            string mensaje = null;
            try
            {
                var result = _rolRepository.AsignarVentanas(idRol, idPermisos, usuarioAuditoria, ref mensaje);
                if (result == null) throw new Exception(mensaje);
                return (result, null);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return (null, mensaje);
            }
        }
    }
}
