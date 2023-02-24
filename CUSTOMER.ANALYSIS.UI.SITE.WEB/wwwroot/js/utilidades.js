//LOADINNG
function IniciarLoading(timer = 50000) {
    Swal.fire({
        icon: "info",
        allowOutsideClick: false,
        title: 'Procesando Información',
        text: 'Espere, por favor!...',
        timer: timer
    });
    Swal.showLoading();
}

function DetenerLoading() {
    Swal.close();
}

function MensajeSesionFinalizada() {
    Swal.fire({
        icon: 'error',
        title: 'Alerta',
        text: 'La sesión ha caducado',
        type: "error",
        timer: 3000,
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    });
}

function MensajeSesionFinalizadaConfirmacionRedireccion(url) {
    Swal.fire({
        title: 'Alerta',
        text: 'La sesión ha caducado',
        icon: 'error',
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    }).then(function () {
        window.location.href = url;
    });
}

function MensajeExitosoSwalConfirmacionRedireccion(Mensaje, modulo, url) {
    Swal.fire({
        title: modulo,
        html: Mensaje,
        icon: 'success',
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    }).then(function () {
        window.location.href = url;
    });
}

function MensajeGrowl(Mensaje) {
    DetenerLoading();
    $.toast({
        heading: '¡Alerta!',
        text: Mensaje,
        position: 'top-right',
        stack: 6,
        hideAfter: 5000,
        showHideTransition: 'fade',
        icon: 'error'
    });
}

function MensajeGrowlSwal(Mensaje) {
    DetenerLoading();
    Swal.fire({
        icon: 'error',
        title: 'INTERCOM',
        html: Mensaje,
        type: "error",
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    });
}

function MensajeErrorSwal(mensaje, modulo) {
    DetenerLoading();
    Swal.fire({
        icon: "error",
        title: modulo,
        html: mensaje
    });
}

function MensajeExitoso(Mensaje, position = 'top-right') {
    DetenerLoading();
    $.toast({
        heading: 'Exitoso',
        text: Mensaje,
        position: position,
        stack: 5,
        showHideTransition: 'fade',
        icon: 'success'
    });
}

function MsgAjaxError(result) {
    DetenerLoading();
    var msg = "Existió un error, favor intentelo de nuevo.";
    var UrlBaseSite = $("#UrlBaseSite").val();
    try {
        if (result.status === 401) {
            MensajeSesionFinalizadaConfirmacionRedireccion(UrlBaseSite);
        } else if (result.status === 403) {
            MensajeGrowl('El usuario no tiene permiso para acceder a este contenido');
        } else if (result.status === 404) {
            MensajeGrowl('Error al procesar la solicitud.');
        } else {
            MensajeGrowl(msg);
        }
    } catch (e) {
        MensajeGrowl(e);
    }
}

function ConfirmacionEliminar(urlAction, urlRedirect, data) {
    Swal.fire({
        title: 'Eliminar registro',
        text: "¿Está seguro de eliminar este registro? ¡No podrás revertir esto!",
        icon: 'question',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar'
    }).then((result) => {
        if (result.value) {
            IniciarLoading();
            $.post(urlAction, data, function (response) {
                DetenerLoading();
                if (response.estado) {
                    MensajeExitosoSwalConfirmacionRedireccion(response.mensaje, "", urlRedirect);
                } else {
                    MensajeGrowl(response.mensaje);
                }
            }).fail(function (error) {
                MsgAjaxError(error);
            });
        }
    });
}

function ConfirmacionEditar(urlAction, urlRedirect, data) {
    Swal.fire({
        title: 'Editar registro',
        text: "¿Está seguro de editar este registro? ¡No podrás revertir esto!",
        icon: 'question',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'EditarAgente'
    }).then((result) => {
        if (result.value) {
            IniciarLoading();
            $.post(urlAction, data, function (response) {
                DetenerLoading();
                if (response.estado) {
                    MensajeExitosoSwalConfirmacionRedireccion(response.mensaje, "", urlRedirect);
                } else {
                    MensajeGrowl(response.mensaje);
                }
            }).fail(function (error) {
                MsgAjaxError(error);
            });
        }
    });
}

function isEmpty(val) {
    return (val === undefined || val === null || val.length <= 0 || val.length === "") ? true : false;
}

// Rutina para agregar opciones a un <select>
function addOptionsProductos(domElement, result) {
    var select = document.getElementById(domElement);
    LimpiarSelect(domElement);
    result.forEach(producto => {
        var option = document.createElement("option");
        option.value = producto.productoId;
        option.text = producto.codigo + " - " + producto.descripcion;
        option.setAttribute("data-stock", producto.stock);
        select.add(option);
    });
}

function addOptionsSucursales(domElement, result) {
    var select = document.getElementById(domElement);
    LimpiarSelect(domElement);
    result.forEach(sucursal => {
        var option = document.createElement("option");
        option.value = sucursal.sucursalId;
        option.text = sucursal.nombre;
        select.add(option);
    });
}

function addOptionsBodegas(domElement, result) {
    var select = document.getElementById(domElement);
    LimpiarSelect(domElement);
    result.forEach(bodega => {
        var option = document.createElement("option");
        option.value = bodega.bodegaId;
        option.text = bodega.codigo + " - " + bodega.nombre;
        select.add(option);
    });
}

function addOptionsProductosCliente(domElement, result) {
    var select = document.getElementById(domElement);
    LimpiarSelect(domElement);
    result.forEach(producto => {
        var option = document.createElement("option");
        option.value = producto.productoClienteId;
        option.text = producto.codigo + " - " + producto.nombre;
        //option.setAttribute("data-stock", producto.stock);
        select.add(option);
    });
}

function LimpiarSelect(domElement) {
    $('#' + domElement).empty();
    var select = document.getElementById(domElement);
    var option = document.createElement("option");
    option.text = "--seleccionar--";
    option.value = "";
    select.add(option);
}

function uuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

var formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
});

function createDate(dateString) {
    /*
      Dividimos la cadena por el separador
      */
    //console.log(dateString);
    var dateParts = dateString.split("-");
    /* 
      En Javascript el mes es 0-based, 
      por eso restamos 1 a la parte del mes
      mediante dataParts[1] - 1
      Y usamos Date.UTC para que ignore la zona horaria
      esto puedes quitarlo si no interesa
      */
    return new Date(Date.UTC(+dateParts[2], dateParts[1] - 1, +dateParts[0]));
}

$('.input-number').on('input', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

$(".input-texto").bind('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});