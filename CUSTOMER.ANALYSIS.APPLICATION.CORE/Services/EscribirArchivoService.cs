
using Ionic.Zip;
using AGRICOLA.LOLANDIA.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using Humanizer.Bytes;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Services
{
    public class EscribirArchivoService : IEscribirArchivoService
    {

        public EscribirZipResponseDTO _response;

        public EscribirArchivoService()
        {
            _response = new EscribirZipResponseDTO();
        }

        public string EscribirZip(string pathDirectorio)
        {
            // pathDirectorio = pathDirectorio + ".zip";
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(pathDirectorio);
                zip.Save(pathDirectorio + ".zip");
                zip.Dispose();
            }

            if (System.IO.File.Exists(pathDirectorio + ".zip"))
            {
                long fileLenghtBytes = new System.IO.FileInfo(pathDirectorio + ".zip").Length;
                _response.TieneZip = true;
                _response.LenghtBytes = ByteSize.FromBytes(fileLenghtBytes).Bytes;
                _response.LenghtKb = ByteSize.FromBytes(fileLenghtBytes).Kilobytes;
                _response.LenghtMb = ByteSize.FromBytes(fileLenghtBytes).Megabytes;
                _response.RutaLocal = pathDirectorio + ".zip";
                return pathDirectorio + ".zip";
            }
            else
            {
                _response.TieneZip = false;
                //_log.LogError("El archivo no existe en el directorio especificado.");
            }
            return "--NO_ZIP";
        }

        public EscribirZipResponseDTO ResponseEscribirZip(List<ArchivoResponseDTO> archivos, string rutaBase, string subdirectorio, string carpeta)
        {
            try
            {
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                if (!Directory.Exists(rutaBase + subdirectorio + carpeta))
                {
                    Directory.CreateDirectory(rutaBase + subdirectorio + carpeta);
                }

                byte[] fileStream = null;
                // directorio del documento descargado
                string pathDirectorio = rutaBase + subdirectorio + carpeta;
                int archivosEncontrados = 0;
                int archivosNoEncontrados = 0;
                foreach (var archivo in archivos)
                {
                    fileStream = archivo.Archivo;
                    if (fileStream != null)
                    {
                        archivosEncontrados++;
                        _response.CantidadArchivosZipeados = archivosEncontrados;
                        //string PathCarpetaDocumento =  Path.Combine(pathDirectorio, archivo.NombreArchivo);

                        //if (!Directory.Exists(PathCarpetaDocumento))
                        //{
                        //    Directory.CreateDirectory(PathCarpetaDocumento);
                        //}

                        System.IO.File.WriteAllBytes(pathDirectorio + "/" + archivo.NombreArchivo, fileStream);
                    }
                    else
                    {
                        archivosNoEncontrados++;
                        _response.CantidadArchivosNoZipeados = archivosNoEncontrados;
                        _response.ArchivosNoZipeados.Add(archivo.NombreArchivo);
                    }
                }

                // proceso que genera el zip a partir de la creacion de un directorio

                string zip = EscribirZip(pathDirectorio);
                if (zip == null)
                {
                    zip = "--NO_ZIP";

                }

                _response.EstadoSolicitud = true;
            }
            catch (Exception ex)
            {
                _response.TieneError = true;
                _response.MensajeErrorPrincipal = ex.Message;
                _response.MensajeErrorSecundario = ex.InnerException == null ? "" : ex.InnerException.Message;

            }
            return _response;
        }

    }
}
