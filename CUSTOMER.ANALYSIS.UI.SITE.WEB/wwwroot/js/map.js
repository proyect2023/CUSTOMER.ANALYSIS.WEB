"use strict";

let map, heatmap;
let urlClientes = "";
let resultados = [];
let clintes = [];

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
        clintes = response;
        initMap(response);
    }).fail(function (error) {
        console.error(error);
    })
}

function getAllPoints(initialData = []) {
    var locs = [];
    resultados = [];
    initialData.forEach(x => {
        resultados.push({ latitud: x.latitud, longitud: x.longitud });
        locs.push(new google.maps.LatLng(x.latitud, x.longitud));
    });
    console.log('Cantidad de consultas', resultados.length);
    return locs;
}

function initMap(initialData = []) {

    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 15,
        center: {
            lat: -2.167485,
            lng: -79.462470
        },
        mapTypeId: "satellite"
    });

    // Create an info window to share between markers.
    const infoWindow = new google.maps.InfoWindow();
    
    // Create the markers.();
    initialData.forEach(item => {
        const marker = new google.maps.Marker({
            position: {
                lat: item.latitud,
                lng: item.longitud
            },
            map,
            title: `<p><b>Nombres</b>: ${item.identificacion} - ${item.razonSocial}</p><p><b>Teléfono</b>: ${item.telefono}</p><p><b>Coordenadas</b>: ${item.latitud}, ${item.longitud}</p>`,
            //label: item.razonSocial,
            optimized: false,
        });

        // Add a click listener for each marker, and set up the info window.
        marker.addListener("click", () => {
            infoWindow.close();
            infoWindow.setContent(marker.getTitle());
            infoWindow.open(marker.getMap(), marker);
        });
    });

    window.initMap = initMap;
    
}


//function onChangeMasVendidos() {
//    if ($('#chk-clientes-con-ventas').prop('checked')) {
//        masVendidos = true;
//    } else {
//        masVendidos = false;
//    }
//    getClientes();
//}

//function onChangeAntiguos() {
//    if ($('#chk-clientes-antiguos').prop('checked')) {
//        antiguos = true;
//    } else {
//        antiguos = false;
//    }
//    getClientes();
//}

//function onChangeInactivos() {
//    if ($('#chk-clientes-inactivos').prop('checked')) {
//        estadoClientePlan = 2;
//    } else {
//        estadoClientePlan = 1;
//    }
//    getClientes();
//}

//function onChangeSector(id) {
//    if ($('#chk-sector-' + id).prop('checked')) {
//        sector = id;
//    } else {
//        sector = 0;
//    }
//    getClientes();
//}

window.initMap = initMap;