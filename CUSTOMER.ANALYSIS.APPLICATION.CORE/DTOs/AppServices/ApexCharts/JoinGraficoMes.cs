using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices.ApexCharts
{
    public class JoinGraficoMes
    {

        public List<GraficoMes> Meses;

        public JoinGraficoMes()
        {
            this.Meses = new List<GraficoMes>();
            Meses.Add(new GraficoMes { NombreCompleto = "Enero", NombreCorto = "Ene", NumeroMes = 1 });
            Meses.Add(new GraficoMes { NombreCompleto = "Febrero", NombreCorto = "Feb", NumeroMes = 2 });
            Meses.Add(new GraficoMes { NombreCompleto = "Marzo", NombreCorto = "Mar", NumeroMes = 3 });
            Meses.Add(new GraficoMes { NombreCompleto = "Abril", NombreCorto = "Abr", NumeroMes = 4 });
            Meses.Add(new GraficoMes { NombreCompleto = "Mayo", NombreCorto = "May", NumeroMes = 5 });
            Meses.Add(new GraficoMes { NombreCompleto = "Junio", NombreCorto = "Jun", NumeroMes = 6 });
            Meses.Add(new GraficoMes { NombreCompleto = "Julio", NombreCorto = "Jul", NumeroMes = 7 });
            Meses.Add(new GraficoMes { NombreCompleto = "Agosto", NombreCorto = "Ago", NumeroMes = 8 });
            Meses.Add(new GraficoMes { NombreCompleto = "Septiembre", NombreCorto = "Sep", NumeroMes = 9 });
            Meses.Add(new GraficoMes { NombreCompleto = "Octubre", NombreCorto = "Oct", NumeroMes = 10 });
            Meses.Add(new GraficoMes { NombreCompleto = "Noviembre", NombreCorto = "Nov", NumeroMes = 11 });
            Meses.Add(new GraficoMes { NombreCompleto = "Diciembre", NombreCorto = "Dic", NumeroMes = 12 });
        }

        //public List<GraficoMesAnual> LeftJoin(List<GraficoMesAnual> ListaIzquierda, List<GraficoMesAnual> ListaDerecha) {
        //    var query = from izquierdo in ListaIzquierda
        //                join derecho in ListaDerecha on izquierdo.NumeroMes equals derecho.NumeroMes into result
        //                from x in result.DefaultIfEmpty()
        //                select new GraficoMesAnual
        //                {
        //                    NombreCorto = izquierdo.NombreCorto,
        //                    NumeroMes = izquierdo.NumeroMes,
        //                    ValorTotal = (x == null ? 0 : x.ValorTotal)
        //                };
        //    return query.ToList();
        //}

        public List<GraficoMes> LeftJoin(List<GraficoMes> ListaDerecha)
        {
            var query = from izquierdo in Meses
                        join derecho in ListaDerecha on izquierdo.NumeroMes equals derecho.NumeroMes into result
                        from x in result.DefaultIfEmpty()
                        select new GraficoMes
                        {
                            NombreCorto = izquierdo.NombreCorto,
                            NumeroMes = izquierdo.NumeroMes,
                            ValorTotal = (x == null ? 0 : x.ValorTotal)
                        };
            return query.ToList();
        }

        public List<GraficoMes> LeftJoinDecimal(List<GraficoMes> ListaDerecha)
        {
            var query = from izquierdo in Meses
                        join derecho in ListaDerecha on izquierdo.NumeroMes equals derecho.NumeroMes into result
                        from x in result.DefaultIfEmpty()
                        select new GraficoMes
                        {
                            NombreCorto = izquierdo.NombreCorto,
                            NumeroMes = izquierdo.NumeroMes,
                            ValorTotalDecimal = (x == null ? 0 : x.ValorTotalDecimal)
                        };
            return query.ToList();
        }
    }

    //public class JoinGraficoPlanes
    //{
    //    public List<GraficoPlan> Planes;

    //    public JoinGraficoPlanes()
    //    {
    //        Planes = 
    //    }

    //    public List<GraficoMes> LeftJoinDecimal(List<GraficoPlan> ListaDerecha)
    //    {
    //        var query = from izquierdo in Planes
    //                    join derecho in ListaDerecha on izquierdo.NumeroMes equals derecho.NumeroMes into result
    //                    from x in result.DefaultIfEmpty()
    //                    select new GraficoMes
    //                    {
    //                        NombreCorto = izquierdo.NombreCorto,
    //                        NumeroMes = izquierdo.NumeroMes,
    //                        ValorTotalDecimal = (x == null ? 0 : x.ValorTotalDecimal)
    //                    };
    //        return query.ToList();
    //    }
    //}
}
