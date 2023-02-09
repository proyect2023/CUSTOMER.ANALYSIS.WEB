using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class PermisoAppResultDto
    {
        public string Id { get; set; }
        public string NombreAbreviado { get; set; }
        public long Codigo { get; set; }
        public string Icono { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public long? IdPadre { get; set; }

        public PermisoAppResultDto(Permisos model)
        {
            Id = Crypto.CifrarId(model.IdPermiso);
            NombreAbreviado = model.NombreAbreviado;
            Icono = model.Icono;
            Codigo = model.Codigo ?? 0;
            Descripcion = model.Descripcion;
            Url = model.Url;
            IdPadre = model.IdPadre ?? 0;
        }
    }
}
