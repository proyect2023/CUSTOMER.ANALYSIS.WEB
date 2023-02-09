/***
* Uso de SignalR para las notificaciones en tiempo real
* */
"use strict";
var urlBaseHub = "";

function connectionSignal(url) {
    
    return new signalR.HubConnectionBuilder().withUrl(url + "EscanerCodigoAreteHub").build();
}

const initSignal = () => {

    if (urlBaseHub !== "/") {
        urlBaseHub += "/";
    }

    let connection = connectionSignal(urlBaseHub);

    connection.start().then(function () {
        console.log("Conexión exitosa");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    connection.on("ReceiveMessageCodigoArete", function (data) {
        if (data === "SqlNotificationInfo.Insert" || data === "SqlNotificationInfo.Update") {
            cargarCodigoAreteEscaneado(urlBaseHub);
        }
    });
}

const cargarCodigoAreteEscaneado = (url) => {
    IniciarLoading();

    $.post(url + "Animal/CargarCodigoAreteEscaneado").done(function (response) {
        DetenerLoading();
        ejecutarAccion(response.codigo);
        
    }).fail(function (error) {
        DetenerLoading();
    })
}

const buscarAnimalPorCodigo = (codigo, elementSelect = null) => {
    $.post(urlBaseHub + "Animal/BuscarAnimalPorCodigo", { codigo }).done(function (response) {

        if (response) {
            const { idAnimal } = response;
            if (elementSelect) {
                $(elementSelect).val(idAnimal).trigger('change');
            }
        }

        return response;
    }).fail(function (error) {
        return null;
    })
}
