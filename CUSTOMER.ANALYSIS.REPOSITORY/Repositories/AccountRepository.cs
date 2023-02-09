using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using GS.TOOLS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class AccountRepository : Repository<Usuario>, IAccountRepository
    {
        public AccountRepository(EFContext context) : base(context)
        {
        }

        public List<VentanaLoginDto> ConsultarVentana(long IdUsuario, ref string mensaje)
        {
            try
            {
                var rol = _context.UsuarioRol.Include(x => x.IdRolNavigation).FirstOrDefault(x => x.Estado == true && x.IdUsuario == IdUsuario);
                var permisos = _context.RolPermiso.Include(x => x.IdPermisoNavigation).Where(x => x.Estado == true && x.IdRol == rol.IdRol);

                return permisos.Where(x => x.IdPermisoNavigation.Estado == 1).Select(c => new VentanaLoginDto
                {
                    Codigo = c.IdPermisoNavigation.Codigo ?? 0,
                    Icono = c.IdPermisoNavigation.Icono ?? "",
                    IdPadre = c.IdPermisoNavigation.IdPadre,
                    IdPermiso = c.IdPermiso ?? 0,
                    IdRol = rol.IdRol ?? 0,
                    NombreAbreviado = c.IdPermisoNavigation.NombreAbreviado ?? "",
                    Rol = rol.IdRolNavigation.Nombre ?? "",
                    Url = c.IdPermisoNavigation.Url ?? "",
                    Orden = c.IdPermisoNavigation.Orden ?? 0
                }).ToList();

                //(from p in _context.Permisos join rp in _context.RolPermiso on p.)
                //var res = (from r in _context.Rol join pr in _context.RolPermiso on r.IdRol equals pr.IdRol);

                ///return _context.ConsultarVentanasXUsuarioLogin(IdUsuario);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return null;
            }
        }

        

        public bool RegistrarAccesoUsuario(AccesoUsuario acceso)
        {
            _context.AccesoUsuario.Add(acceso);
            return _context.SaveChanges() > 0;
        }

    }
}
