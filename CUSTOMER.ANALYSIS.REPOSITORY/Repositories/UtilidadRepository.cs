

using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class UtilidadRepository : IUtilidadRepository
    {
        private readonly EFContext _context;

        public UtilidadRepository(EFContext context)
        {
            this._context = context;
        }

        public void InicializarDb()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
        }

        public bool AddRol(int Id, string Nombre)
        {
            _context.Rol.Add(new Rol
            {
                IdRol = Id,
                Nombre = Nombre,
                FechaCreacion =  APPLICATION.CORE.Utilities.Utilidades.GetHoraActual(),
                UsuarioCreacion = 0,
                Ip = "0.0.0.0",
                Estado = true,
            });

            return _context.SaveChanges() > 0;
        }

        public Parametros GetParametro(string Codigo)
        {
            return _context.Parametros.FirstOrDefault(x => x.Codigo == Codigo && x.Estado);
        }

        public List<Rol> GetRolesPrincipales()
        {
            List<string> rolesPrincipales = new List<string>
            {
                Roles.Administrador.ToString(),
                Roles.Empleado.ToString(),
            };

            return _context.Rol.Where(x => rolesPrincipales.Contains(x.Nombre)).ToList();
        }

        public void AgregarUsuarioAdministrador()
        {

            var usuarios = _context.UsuarioRol
                .Include(x => x.IdUsuarioNavigation)
                .Where(x => x.IdRol == ((int)Roles.Administrador));

            if (usuarios == null || !usuarios.Any())
            {
                var usuarioAdmin = new Usuario
                {
                    Username = "Admin",
                    Nombre = "Administrador",
                    Apellido = "Administrador",
                    Estado = (EstadoUsuario.Activo),
                    CorreoElectronico = "",
                    FechaCreacion = APPLICATION.CORE.Utilities.Utilidades.GetHoraActual(),
                    UsuarioCreacion = 0,
                    Ip = "",
                    Password = GS.TOOLS.GSCrypto.ComputeHashV1("$A01!.gCczZa#@k"),
                };

                _context.Usuario.Add(usuarioAdmin);

                if (_context.SaveChanges() > 0)
                {
                    _context.UsuarioRol.Add(new UsuarioRol
                    {
                        Estado = true,
                        IdRol = ((int)Roles.Administrador),
                        IdUsuario = usuarioAdmin.IdUsuario
                    });

                    if (_context.SaveChanges() > 0)
                    {
                        var permisos = _context.Permisos.Where(x => x.Estado == 1).ToList();
                        List<RolPermiso> permisosRoles = new List<RolPermiso>();

                        foreach (var permiso in permisos)
                        {
                            permisosRoles.Add(new RolPermiso
                            {
                                Estado = true,
                                FechaCreacion = APPLICATION.CORE.Utilities.Utilidades.GetHoraActual(),
                                UsuarioCreacion = usuarioAdmin.IdUsuario,
                                IdPermiso = permiso.IdPermiso,
                                IdRol = ((int)Roles.Administrador)
                            });
                        }

                        _context.RolPermiso.AddRange(permisosRoles);
                        _context.SaveChanges();
                    }
                }
            }
        }

    }
}
