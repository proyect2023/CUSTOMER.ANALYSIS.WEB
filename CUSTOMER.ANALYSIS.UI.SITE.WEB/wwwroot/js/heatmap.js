"use strict";

let map, heatmap;
let urlClientes = "";
let resultados = [];
let masVendidos = false;
let antiguos = false;
let estadoClientePlan = 1;
let sector = 0;

function initMap(initialData = []) {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 15,
        center: {
            lat: -2.167485,
            lng: -79.462470
        },
        mapTypeId: "satellite"
    });
    heatmap = new google.maps.visualization.HeatmapLayer({
        data: initialData,
        map: map
    });
    heatmap.set("radius", heatmap.get("radius") ? null : 18);
}

function getClientes() {
    $.get(urlClientes + '/Get', { masVendidos, antiguos, estadoClientePlan, sector }).done(function (response) {
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

function onChangeMasVendidos() {
    if ($('#chk-clientes-con-ventas').prop('checked')) {
        masVendidos = true;
    } else {
        masVendidos = false;
    }
    getClientes();
}

function onChangeAntiguos() {
    if ($('#chk-clientes-antiguos').prop('checked')) {
        antiguos = true;
    } else {
        antiguos = false;
    }
    getClientes();
}

function onChangeInactivos() {
    if ($('#chk-clientes-inactivos').prop('checked')) {
        estadoClientePlan = 2;
    } else {
        estadoClientePlan = 1;
    }
    getClientes();
}

function onChangeSector(id) {
    if ($('#chk-sector-' + id).prop('checked')) {
        sector = id;
    } else {
        sector = 0;
    }
    getClientes();
}