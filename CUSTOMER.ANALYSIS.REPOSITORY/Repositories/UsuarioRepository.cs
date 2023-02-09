
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using GS.TOOLS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EFContext context) : base(context)
        {

        }

        public List<Usuario> GetUsuariosAdministrador()
        {
            return (from usuario in _context.Usuario
                    join usuarioRol in _context.UsuarioRol on usuario.IdUsuario equals usuarioRol.IdUsuario
                            join rol in _context.Rol on usuarioRol.IdRol equals rol.IdRol
                            where 
                                rol.IdRol == ((int)Roles.Administrador)
                            select usuario
                            ).ToList();
        }

        //Editar Usuario
        public bool EditarUsuario(Usuario editar, ref string mensaje)
        {
            try
            {
                _context.Update(editar);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return false;
            }
        }

        public List<Usuario> ConsultarUsuarios()
        {
            return _context.Usuario
                .Include(x => x.UsuarioRol)
                .Include(x => x.UsuarioRol).ThenInclude(x => x.IdRolNavigation)
                .Where(x => x.Estado != (EstadoUsuario.Eliminado)).ToList();
        }

        public Usuario? ObtenerUsuario(long id)
        {
            return _context.Usuario
                .Include(x => x.UsuarioRol)
                .Include(x => x.UsuarioRol).ThenInclude(x => x.IdRolNavigation)
                .Where(x => x.IdUsuario == id && x.Estado != (EstadoUsuario.Eliminado)).FirstOrDefault();
        }

        //Obtener Usuarios
        public Usuario ObtenerUsuario(long idUsuario, ref string mensaje)
        {
            return _context.Usuario.Where(x => x.IdUsuario == idUsuario).FirstOrDefault();
        }

        //Obtener Codigo Usuario
        public List<Usuario> ObtenerCodigoUsuario()
        {
            return _context.Usuario.Where(x => x.Estado == EstadoUsuario.Activo).ToList();
        }

        public bool GuardarUsuarioRol(UsuarioRol model)
        {
            var result = _context.UsuarioRol.FirstOrDefault(x => x.IdUsuario == model.IdUsuario && x.Estado == true);
            if (result == null)
            {
                _context.UsuarioRol.Add(model);
            }
            else
            {
                result.IdUsuario = model.IdUsuario;
                result.IdRol = model.IdRol;
                _context.UsuarioRol.Update(result);
            }

            return _context.SaveChanges() > 0;
        }
    }
}
