"use strict";

let map, heatmap;
let urlClientes = "";
let resultados = [];

function initMap(initialData = []) {
    var options = {
        zoom: 15,
        center: {
            lat: -2.167485,
            lng: -79.462470
        },
        mapTypeId: "satellite"
    };
    map = new google.maps.Map(document.getElementById("map"), options);
    heatmap = new google.maps.visualization.HeatmapLayer({
        data: initialData,
        map: map
    });
    heatmap.set("radius", heatmap.get("radius") ? null : 18);
}

function getClientes() {
    const validaciones = $("#sel-validaciones").val();
    const sectores = $("#sel-sectores").val();
    const planes = $("#sel-planes").val();

    console.log('validaciones', validaciones);
    console.log('sectores', sectores);
    console.log('planes', planes);

    $.post(urlClientes + '/Get', {
        validaciones: validaciones,
        sectores: sectores,
        planes: planes
    }).done(function (response) {
        //console.log(response);
        /*resultados = response;*/
        initMap(getAllPoints(response));
    }).fail(function (error) {
        console.error(error);
    })
}

function getAllPoints(initialData = []) {
    var locs = [];
    resultados = [];
    initialData.forEach(x => {
        resultados.push({ latitud: x.latitud, longitud: x.longitud});
        locs.push(new google.maps.LatLng(x.latitud, x.longitud));
    });
    console.log('Cantidad de consultas', resultados.length);
    return locs;
}
