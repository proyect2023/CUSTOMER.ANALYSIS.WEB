

using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities.StoreProcedures;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using GS.TOOLS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public RolRepository(IServiceScopeFactory serviceScopeFactory, EFContext context) : base(context)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public List<Rol> GetRoles()
        {
            return _context.Rol.Where(x => x.Estado == true).ToList();
        }

        public bool ActualizaRol(SPRol rol, ref string mensaje)
        {
            try
            {
                //using (var scope = serviceScopeFactory.CreateScope())
                //{
                //    using var edocCmdContext = scope.ServiceProvider.GetRequiredService<EFContext>();
                //    edocCmdContext.ActualizarRol(rol);
                //}
                var r = _context.Rol.FirstOrDefault(x => x.IdRol == rol.IdRol && x.Estado == true);
                r.Estado = rol.Estado;
                r.Nombre = rol.Nombre;

                _context.Rol.Update(r);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return false;
            }
        }

        public bool RegistrarRol(SPRol rol, ref string mensaje)
        {
            try
            {
                _context.Rol.Add(new Rol
                {
                    IdRol = _context.Rol.Max(x => x.IdRol) + 1,
                    Nombre = rol.Nombre,
                    Ip = "",
                    Estado = true,
                    FechaCreacion = APPLICATION.CORE.Utilities.Utilidades.GetHoraActual(),
                    //UsuarioCreacion = rol.IdUsuario
                });

                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return false;
            }
        }

        public int? AsignarVentanas(long idRol, string idPermisos, long usuarioAuditoria, ref string mensaje)
        {
            try
            {
                var permisos = idPermisos.Split(";").Where(x => !string.IsNullOrEmpty(x)).Select(x => long.Parse(x)).ToArray();

                List<RolPermiso> permisosRol = _context.RolPermiso.Where(x => x.IdRol == idRol && x.Estado == true).ToList();
                permisosRol.ForEach(x => x.Estado = false);
                _context.RolPermiso.UpdateRange(permisosRol);
                _context.SaveChanges();

                //_context.Permisos.Where(x => permisos.Contains(x.IdPermiso) && );

                 permisosRol =  permisos.Select(c => new RolPermiso
                {
                    IdRol = idRol,
                    IdPermiso = c,
                    Estado = true,
                    UsuarioCreacion = usuarioAuditoria,
                    FechaCreacion = APPLICATION.CORE.Utilities.Utilidades.GetHoraActual()
                }).ToList();


                _context.RolPermiso.AddRange(permisosRol);
                return _context.SaveChanges();

                //using (var scope = serviceScopeFactory.CreateScope())
                //{
                //    using (var edocCmdContext = scope.ServiceProvider.GetRequiredService<EFContext>())
                //    {
                //        return edocCmdContext.AsignarVentanas(idRol, idPermisos, usuarioAuditoria);
                //    }
                //}
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        public List<Rol> ObtenerCodigoRol()
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<EFContext>())
                    {
                        var obtener = context.Rol.Where(x => x.Estado == true).ToList();
                        return obtener;
                    }
                }
            }
            catch (Exception ex)
            {
                //mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        public List<Rol> GetRolesUsuario(long IdUsuario)
        {
            var result = _context.UsuarioRol.Include(x => x.IdRolNavigation).Where(x => x.IdUsuario == IdUsuario && x.Estado == true);
            if(result != null)
            {
                return result.Select(x => x.IdRolNavigation).ToList();
            }
            return null;
        }
    }
}
