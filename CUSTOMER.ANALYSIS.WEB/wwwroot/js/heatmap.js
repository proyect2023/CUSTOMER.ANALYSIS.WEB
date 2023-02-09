"use strict";

let map, heatmap;
let urlClientes = "";

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
    $.get(urlClientes).done(function (response) {
        console.log(response);
        initMap(getAllPoints(response));
    }).fail(function (error) {
        console.error(error);
    })
}

function getAllPoints(initialData = []) {
    var locs = [];
    initialData.forEach(x => {
        locs.push(new google.maps.LatLng(x.latitud, x.longitud));
    });
    console.log(locs);
    return locs;
}

