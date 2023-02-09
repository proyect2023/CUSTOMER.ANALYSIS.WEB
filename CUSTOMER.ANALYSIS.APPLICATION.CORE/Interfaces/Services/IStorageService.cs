

using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services
{
    public interface IStorageService
    {
        bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje, string containerName = null);
        bool EliminarArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        bool GenerarDirectorio(string RutaDirectorio, ref string mensaje, string containerName = null);
        List<ArchivoServiceDto> GuardarImagenes(List<IFormFile> imagenes, string Tipo, string NombreArchivo = "");
        string ObtenerImagenBase64(string TipoAlmacenamiento, string Imagen, string FileName);
    }
}
