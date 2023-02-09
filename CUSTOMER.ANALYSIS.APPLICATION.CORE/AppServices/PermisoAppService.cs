using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class PermisoAppService : BaseAppService, IPermisoAppService
    {
        private readonly IPermisoRepository permisoRepository;

        public PermisoAppService(IPermisoRepository permisoRepository)
        {
            this.permisoRepository = permisoRepository;
        }

        public (List<PermisoAppResultDto>, string) ListarPermisos()
        {   
            try
            {
                var result = permisoRepository.GetAllAsNoTracking(x => x.Estado == 1).ToList();
                if (result == null) throw new Exception("Object null");
                return (result.Select(c => new PermisoAppResultDto(c)).ToList(), null);
            }
            catch (Exception ex)
            {
                return (null, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }

        public (List<(long, string)>, string) VerPermisos()
        {
            try
            {
                List<(long, string)> lista = new List<(long, string)>();
                var result = permisoRepository.GetAllAsNoTracking(x => x.Estado == 1).ToList();
                if (result == null) throw new Exception("Object null");
                foreach (var item in result)
                {
                    lista.Add((item.IdPermiso, item.NombreAbreviado));
                }
                
                return (lista, null);
            }
            catch (Exception ex)
            {
                return (null, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }


        public (PermisoAppResultDto, string) VerPermiso(string IdCifrado)
        {
            try
            {
                long Id = long.Parse(Crypto.DescifrarId(IdCifrado));
                var result = permisoRepository.Get(Id);
                if (result == null) throw new Exception("Object null");
                return (new PermisoAppResultDto(result), null);
            }
            catch (Exception ex)
            {
                return (null, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }

        public (bool, string) AgregarPermiso(string NombreAbreviado, long? Codigo, string Icono, string Descripcion, string Url, long? IdPadre, long IdUsuario, string Ip)
        {
            var permiso = permisoRepository.GetFirstOrDefault(x => x.NombreAbreviado == NombreAbreviado && x.Estado == 1);
            if (permiso != null)
            {
                return (false, "Ya existe un permiso agregado con este nombre");
            }

            permiso = permisoRepository.GetFirstOrDefault(x => x.Codigo == Codigo && x.Estado == 1);
            if (permiso != null)
            {
                return (false, "Ya existe un permiso agregado con este Código");
            }

            permiso = new Entities.Permisos
            {
                Codigo = Codigo,
                NombreAbreviado = NombreAbreviado,
                Icono = Icono,
                Descripcion = Descripcion,
                Url = Url,
                IdPadre = IdPadre,

                FechaCreacion = CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities.Utilidades.GetHoraActual(),
                UsuarioCreacion = IdUsuario,
                Ip = Ip,
                Estado = 1
            };
            permisoRepository.Add(permiso);

            return (permisoRepository.Save() > 0, null);
        }

        public (bool, string) EditarPermiso(string IdCifrado, string NombreAbreviado, long? Codigo, string Icono, string Descripcion, string Url, long? IdPadre, long IdUsuario, string Ip)
        {
            long Id = long.Parse(Crypto.DescifrarId(IdCifrado));
            var result = permisoRepository.Get(Id);
            if (result == null) return (false, "No existe el permiso");

            if (result.NombreAbreviado != NombreAbreviado)
            {
                var permiso = permisoRepository.GetFirstOrDefault(x => x.NombreAbreviado == NombreAbreviado && x.Estado == 1);
                if (permiso != null)
                {
                    return (false, "Ya existe un permiso agregado con este nombre");
                }
            }

            if (result.Codigo != Codigo)
            {
                var permiso = permisoRepository.GetFirstOrDefault(x => x.Codigo == Codigo && x.Estado == 1);
                if (permiso != null)
                {
                    return (false, "Ya existe un permiso agregado con este Código");
                }
            }

            result.Codigo = Codigo;
            result.NombreAbreviado = NombreAbreviado;
            result.Icono = Icono;
            result.Descripcion = Descripcion;
            result.Url = Url;
            result.IdPadre = IdPadre;

            result.FechaModificacion = CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities.Utilidades.GetHoraActual();
            result.UsuarioModificacion = IdUsuario;
            result.Ip = Ip;
            permisoRepository.Update(result);

            return (permisoRepository.Save() > 0, null);
        }

        public (bool, string) EliminarPermiso(string IdCifrado, long IdUsuario, string Ip)
        {
            try
            {
                long Id = long.Parse(Crypto.DescifrarId(IdCifrado));
                var result = permisoRepository.Get(Id);
                if (result == null) throw new Exception("Object null");

                result.FechaEliminacion = CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities.Utilidades.GetHoraActual();
                result.UsuarioEliminacion = IdUsuario;
                result.Ip = Ip;
                result.Estado = 0;
                permisoRepository.Update(result);

                return (permisoRepository.Save() > 0, null);
            }
            catch (Exception ex)
            {
                return (false, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }

    }
}
