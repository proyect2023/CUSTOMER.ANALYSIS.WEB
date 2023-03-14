using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Reporte)]
    public class ReporteController : BaseController
    {
        private readonly IMailDomainService _mailDomainService;
        private readonly IReporteService _reporteService;
        private readonly IClienteRepository _clienteRepository;

        public ReporteController(
            IMailDomainService mailDomainService,
            IReporteService reporteService,
            IClienteRepository clienteRepository)
        {
            this._mailDomainService = mailDomainService;
            this._reporteService = reporteService;
            this._clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConsultarVentas(EstadoPlan estado, DateTime? fechaInicio, DateTime? fechaFin)
        {
            fechaFin = new DateTime(fechaFin.Value.Year, fechaFin.Value.Month, fechaFin.Value.Day, 23, 59, 59);
            return PartialView("_Detalle", _clienteRepository.GetClientePlan(fechaInicio.Value, fechaFin.Value, estado));
        }

        [HttpGet]
        public FileResult DownloadReporte(EstadoPlan estado, DateTime? fechaInicio, DateTime? fechaFin, string formato)
        {
            byte[] archivo = new byte[] { };
            string contentType = "";
            fechaFin = new DateTime(fechaFin.Value.Year, fechaFin.Value.Month, fechaFin.Value.Day, 23, 59, 59);
            string filename = $"{Utilidades.GetHoraActual().ToString("yyyyMMddHHmmssfff")}";

            var result = _clienteRepository.GetClientePlan(fechaInicio.Value, fechaFin.Value, estado);

            var resultDownload = _reporteService.DownloadReporte(result, "FormatoReporte.xlsx", fechaInicio.Value, fechaFin.Value, formato);
            if (resultDownload.TieneErrores) throw new Exception(resultDownload.MensajeError);
            if (resultDownload.Estado)
            {
                if (resultDownload.MensajeAdicional == ".zip")
                {
                    string rutaCarpeta = resultDownload.Data;
                    contentType = "application/zip";
                    archivo = System.IO.File.ReadAllBytes(resultDownload.Data);

                    try
                    {
                        System.IO.Directory.Delete(rutaCarpeta.Replace(".zip", ""), true);
                        System.IO.File.Delete(resultDownload.Data);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    contentType = "text/plain";
                    archivo = resultDownload.Data;
                }
            }

            return File(archivo, contentType, filename + resultDownload.MensajeAdicional);
        }

        [HttpPost]
        public IActionResult EnviarReporteMail(string correo, EstadoPlan estado, DateTime? fechaInicio, DateTime? fechaFin, string formato)
        {
            byte[] archivo = new byte[] { };
            string contentType = "";
            fechaFin = new DateTime(fechaFin.Value.Year, fechaFin.Value.Month, fechaFin.Value.Day, 23, 59, 59);
            string filename = $"{Utilidades.GetHoraActual().ToString("yyyyMMddHHmmssfff")}";

            var result = _clienteRepository.GetClientePlan(fechaInicio.Value, fechaFin.Value, estado);

            var resultDownload = _reporteService.DownloadReporte(result, "FormatoReporte.xlsx", fechaInicio.Value, fechaFin.Value, formato);

            if (resultDownload.TieneErrores) throw new Exception(resultDownload.MensajeError);
            if (resultDownload.Estado)
            {
                if (resultDownload.MensajeAdicional == ".zip")
                {
                    string rutaCarpeta = resultDownload.Data;
                    contentType = "application/zip";
                    archivo = System.IO.File.ReadAllBytes(resultDownload.Data);

                    try
                    {
                        System.IO.Directory.Delete(rutaCarpeta.Replace(".zip", ""), true);
                        System.IO.File.Delete(resultDownload.Data);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    contentType = "text/plain";
                    archivo = resultDownload.Data;
                }

                MailDto mail = new MailDto
                {
                    Adjuntos = new List<AdjuntoDto> { new AdjuntoDto { Adjunto = archivo, Nombre = filename + resultDownload.MensajeAdicional } },
                    Correos = correo,
                    Asunto = "Reporte de Ventas",
                    Mensaje = "",
                    Tipo = TipoMail.Reporte,
                    
                };

                _mailDomainService.EnviarMail(mail);
            }


            return Json(new ResponseToViewDto { Estado = true});

        }

    }
}
