
using AGRICOLA.LOLANDIA.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class ReporteService : IReporteService
    {
        private readonly IMailDomainService _mailDomainService;
        private readonly IEscribirArchivoService _escribirArchivoService;
        private readonly IGemboxService _gemboxService;

        public ReporteService(
            IMailDomainService mailDomainService,
            IEscribirArchivoService escribirArchivoService,
            IGemboxService gemboxService)
        {
            this._mailDomainService = mailDomainService;
            this._escribirArchivoService = escribirArchivoService;
            this._gemboxService = gemboxService;
        }

        public MethodResponseDto DownloadReporte(List<ClientePlan> result, string FileName, DateTime fechaInicio, DateTime fechaFin, string format)
        {
            MethodResponseDto responseDto = new();

            try
            {
                if (result.Count > 100)
                {
                    List<ArchivoResponseDTO> archivos = new List<ArchivoResponseDTO>();

                    int cont = 0;
                    var nuevaLista = result.partition(100);
                    foreach (var item in nuevaLista)
                    {
                        cont++;
                        byte[] archivo = new byte[] { };
                        var resultDownload = _gemboxService.ConstruirReporte(item, FileName, format);
                        if (resultDownload.TieneErrores) throw new Exception(resultDownload.MensajeError);
                        if (resultDownload.Estado)
                        {
                            archivo = resultDownload.Data;
                        }

                        archivos.Add(new ArchivoResponseDTO
                        {
                            Archivo = archivo,
                            NombreArchivo = $"{Utilidades.GetHoraActual().ToString("yyyyMMdd")}_{fechaFin.ToString("yyyyMMdd")}_{cont}_{Guid.NewGuid().ToString()}.{format}"
                        });
                    }

                    string directorioDocumentosPyme = @"wwwroot/reportes/";
                    string subdirectorioAdjuntosRecibidos = @"reportes/";
                    string carpetaAzipear = $"{Utilidades.GetHoraActual().ToString("yyyyMMddHHmmssfff")}";

                    var responseEscribirDTO = _escribirArchivoService.ResponseEscribirZip(archivos, directorioDocumentosPyme, subdirectorioAdjuntosRecibidos, carpetaAzipear);
                    if (responseEscribirDTO.EstadoSolicitud && responseEscribirDTO.TieneZip)
                    {
                        if (responseEscribirDTO.LenghtBytes < 30)
                        {
                            responseDto.Mensaje = "--ZIP_VACIO";
                            return responseDto;
                        }

                        string file = responseEscribirDTO.RutaLocal;
                        responseDto.Estado = true;
                        responseDto.Data = file;
                        responseDto.MensajeAdicional = ".zip";
                        return responseDto;
                    }
                    else
                    {
                        responseDto.Mensaje = "--NO_ZIP";
                        return responseDto;
                    }
                }
                else
                {
                    responseDto = _gemboxService.ConstruirReporte(result, FileName, format);
                    responseDto.MensajeAdicional = $".{format}";
                }
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
