using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices.ApexCharts;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class DashboardAppService : IDashboardAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPlanRepository _planRepository;

        public DashboardAppService(IClienteRepository clienteRepository, IPlanRepository planRepository)
        {
            _clienteRepository = clienteRepository;
            _planRepository = planRepository;
        }

        public MethodResponseDto Get()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                DashboardDto dashboardDto = new()
                {
                    Planes = _planRepository.GetPlanesClientes(true),
                    Anios = _planRepository.GetAniosClientePlan() ?? new int[] {}
                };

                responseDto.Data = dashboardDto;
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto GetGraficoLineaComportamientoLineaVentasCompras(int anio)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var ventas = _planRepository.GetClientePlan(anio, true);

                List<GraficoMes> grupoFacturasPorMesVentas = ventas
                    .GroupBy(x => new
                    {
                        Mes = x.FechaContratacion.Value.Month // se realiza la agrupacion por mes
                    })
                    .Select(g => new GraficoMes
                    {
                        NombreCorto = string.Empty,
                        NumeroMes = g.Key.Mes,
                        ValorTotalDecimal = g.Sum(x => x.PagoMensual)
                    }).ToList();

                //List<GraficoMes> grupoFacturasPorMesCompras = compras
                //    .GroupBy(x => new
                //    {
                //        Mes = x.FechaEmision.Value.Month // se realiza la agrupacion por mes
                //    })
                //    .Select(g => new GraficoMes
                //    {
                //        NombreCorto = string.Empty,
                //        NumeroMes = g.Key.Mes,
                //        ValorTotalDecimal = g.Sum(x => x.ValorTotal)
                //    }).ToList();

                var dataJoinGraficoMesFacturaVentas = new JoinGraficoMes().LeftJoinDecimal(grupoFacturasPorMesVentas);
                //var dataJoinGraficoMesNoDebitosCompras = new JoinGraficoMes().LeftJoinDecimal(grupoFacturasPorMesCompras);

                List<SerieApexChart> dataGrafico = new List<SerieApexChart>();

                var dataGraficoFacturasVentas = new SerieApexChart();
                dataGraficoFacturasVentas.Name = "Ventas";
                dataGraficoFacturasVentas.Data = dataJoinGraficoMesFacturaVentas.Select(x => x.ValorTotalDecimal).ToList();
                dataGrafico.Add(dataGraficoFacturasVentas);

                //var dataGraficoNotasCreditosCompras = new SerieApexChart();
                //dataGraficoNotasCreditosCompras.Name = "Compras";
                //dataGraficoNotasCreditosCompras.Data = dataJoinGraficoMesNoDebitosCompras.Select(x => x.ValorTotalDecimal).ToList();
                //dataGrafico.Add(dataGraficoNotasCreditosCompras);

                var categories = dataJoinGraficoMesFacturaVentas.Select(x => x.NombreCorto).ToList();

                var xaxis = new XAxisApexChart
                {
                    Categories = categories
                };

                var response = new GraficoDTO
                {
                    Series = dataGrafico,
                    Xaxis = xaxis
                };

                responseDto.Data = response;
                responseDto.Estado = true;

                //GetGraficoLineaPlanesRentabilidad(anio);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto GetGraficoLineaPlanesRentabilidad(int anio)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var planes = _planRepository.GetClientePlan(anio, true);

                List<GraficoPlan> grupoFacturasPorMesVentas = planes
                    .GroupBy(x => new
                    {
                        Plan = x.IdPlanNavigation?.Nombre // se realiza la agrupacion por nombre de plan
                    })
                    .Select(g => new GraficoPlan
                    {
                        NombreCorto = g.Key.Plan,
                        //NumeroMes = g.Key.Plan,
                        ValorTotalDecimal = g.Sum(x => x.PagoMensual)
                    }).ToList();

                List<SerieApexChart> dataGrafico = new List<SerieApexChart>();

                //List<GraficoPlan> dataJoinGraficoMesFacturaVentas = grupoFacturasPorMesVentas.Select(x => new GraficoPlan
                //{
                    
                //}).ToList();

                var dataGraficoFacturasVentas = new SerieApexChart();
                dataGraficoFacturasVentas.Name = "Planes";
                dataGraficoFacturasVentas.Data = grupoFacturasPorMesVentas.Select(x => x.ValorTotalDecimal).ToList();
                dataGrafico.Add(dataGraficoFacturasVentas);

                var categories = grupoFacturasPorMesVentas.Select(x => x.NombreCorto).ToList();

                var xaxis = new XAxisApexChart
                {
                    Categories = categories
                };

                var response = new GraficoDTO
                {
                    Series = dataGrafico,
                    Xaxis = xaxis
                };

                responseDto.Data = response;
                responseDto.Estado = true;
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
