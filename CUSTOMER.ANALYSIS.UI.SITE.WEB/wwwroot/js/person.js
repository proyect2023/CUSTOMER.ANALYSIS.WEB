$(document).ready(function () {
    cambiarLabel();
    $("#TipoPersona").change(function () {
        cambiarLabel();
    });
    validarTipoIdentificacion();
    $("#TipoIdentificacion").change(function () {
        validarTipoIdentificacion();
    });

    jQuery("#Identificacion").on('input', function (evt) {
        // Allow only numbers.
        jQuery(this).val(jQuery(this).val().replace(/[^0-9]/g, ''));
    });
});

function cambiarLabel() {
    if ($("#TipoPersona").val() == '1') {
        $("#label-RazonSocial").html('Razón Social <span class="text-danger">*</span>');
        $("#label-NombreComercial").html('Nombre Comercial');
    } else {
        $("#label-RazonSocial").html('Nombres <span class="text-danger">*</span>');
        $("#label-NombreComercial").html('Apellidos');
    }
}

function validarTipoIdentificacion() {
    const tipoIdentificacion = $("#TipoIdentificacion").val();

    if (tipoIdentificacion === '04' || tipoIdentificacion === "07") { //RUC || Consumidor final
        $("#Identificacion").attr("data-val-maxlength-max", 13);
        $("#Identificacion").attr("maxlength", 13);
        if ($("#Identificacion").val().length > 13) { $("#Identificacion").val(''); }
    } else if (tipoIdentificacion === '05') { //CEDULA
        $("#Identificacion").attr("data-val-maxlength-max", 10);
        $("#Identificacion").attr("maxlength", 10);
        if ($("#Identificacion").val().length > 10) { $("#Identificacion").val(''); }
    } else {
        $("#Identificacion").attr("data-val-maxlength-max", 25);
        $("#Identificacion").attr("maxlength", 25);
    }
}