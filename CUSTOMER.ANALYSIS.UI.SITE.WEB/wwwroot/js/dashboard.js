var urlGraficoVentas = '';
var urlGraficoPlanes = '';

function CargarGraficos() {
    $("#line_chart_datalabel").empty();

    const anio = $("#selAnios").val();

    $.post(urlGraficoVentas, {
        anio: anio
    })
        .done(function (response) {
            if (response.estado) {
                var seriesLinea = response.data.series;
                var xaxisLinea = response.data.xaxis.categories;
                GraficarLineas(seriesLinea, xaxisLinea, anio);
            } else {
                var para = document.createElement("p");
                para.className = "alert alert-danger";
                var node = document.createTextNode("Ha ocurrido un error al cargar el presente Gráfico. Por favor, notifique el incidente en el Menú de Soporte/Déjanos tus comentarios.");
                para.appendChild(node);
                var element = document.getElementById("graphLineas");
                element.appendChild(para);
            }
        })
        .fail(function (error) { MsgAjaxError(error); });
}

function CargarGraficoPlanes() {
    $("#stacked-column-chart").empty();

    const anio = $("#selAniosPlanes").val();

    $.post(urlGraficoPlanes, {
        anio: anio
    })
        .done(function (response) {
            //console.log(response);
            if (response.estado) {
                var seriesLinea = response.data.series;
                var xaxisLinea = response.data.xaxis.categories;
                GraficarBarras(seriesLinea, xaxisLinea, anio);
            } else {
                var para = document.createElement("p");
                para.className = "alert alert-danger";
                var node = document.createTextNode("Ha ocurrido un error al cargar el presente Gráfico. Por favor, notifique el incidente en el Menú de Soporte/Déjanos tus comentarios.");
                para.appendChild(node);
                var element = document.getElementById("graphLineas");
                element.appendChild(para);
            }
        })
        .fail(function (error) { MsgAjaxError(error); });
}

function GraficarLineas(series, categories, anio) {
    //console.log(series);
    var Linea = {
        chart: {
            height: 380,
            type: "line",
            zoom: {
                enabled: !1
            },
            toolbar: {
                show: !1
            }
        },
        colors: ["#556ee6", "#34c38f"],
        dataLabels: {
            enabled: !0
        },
        stroke: {
            width: [3, 3],
            curve: "straight"
        },
        series: series,
        //title: {
        //    text: "Correspondiente al año " + anio,
        //    align: "left"
        //},
        grid: {
            row: {
                colors: ["transparent", "transparent"],
                opacity: .2
            },
            borderColor: "#f1f1f1"
        },
        markers: {
            style: "inverted",
            size: 6
        },
        xaxis: {
            categories: categories,
            title: {
                text: "Pagos Mensuales"
            }
        },
        yaxis: {
            title: {
                text: "$"
            }, labels: {
                formatter: function (value) {
                    return formatter.format(value);
                }
            },
        },
        legend: {
            position: "top",
            horizontalAlign: "right",
            floating: !0,
            offsetY: -25,
            offsetX: -5
        },
        responsive: [{
            breakpoint: 600,
            options: {
                chart: {
                    toolbar: {
                        show: !1
                    }
                },
                legend: {
                    show: !1
                }
            }
        }]
    };
    (chart = new ApexCharts(document.querySelector("#line_chart_datalabel"), Linea)).render();
}

function GraficarBarras(series, xaxis, anio) {
    var options = {
        chart: {
            height: 359,
            type: "bar",
            stacked: !0,
            toolbar: { show: !1 },
            zoom: {
                enabled: !0
            }
        },
        plotOptions: {
            bar: {
                horizontal: !1,
                columnWidth: "15%",
                endingShape: "rounded"
            }
        },
        dataLabels: {
            enabled: !1
        },
        series: series,
        xaxis:
        {
            categories: xaxis
        },
        yaxis: {
            title: {
                text: "$"
            }, labels: {
                formatter: function (value) {
                    return formatter.format(value);
                }
            },
        },
        //title: {
        //    text: "Correspondiente al año " + anio,
        //    align: "left"
        //},
        colors: ["#556ee6", "#f1b44c", "#34c38f"],
        legend: { position: "bottom" },
        fill: { opacity: 1 }
    };
    (chart = new ApexCharts(document.querySelector("#stacked-column-chart"), options)).render();
}
