
var urlParametros = '';

const onClickGuardarParametro = (id) => {

    const param = {
        Codigo: $("#Codigo_" + id).val(),
        Valor: $("#Valor_" + id).val(),
        Descripcion: $("#Descripcion_" + id).val(),
        EsEntero: $("#EsEntero_" + id).val(),
        EsDecimal: $("#EsDecimal_" + id).val(),
        EsTextoLargo: $("#EsTextoLargo_" + id).val(),
    };

    if (!param.Codigo) {
        MensajeGrowl('Validar que el campo "Valor" sea el correcto');
        $("#Codigo_" + id).focus();
        return;
    }

    if (!param.Valor) {
        MensajeGrowl('Ingrese un valor en el campo "Valor"');
        $("#Valor_" + id).focus();
        return;
    }

    $.post(urlParametros + '/Guardar' , {
        parametro: param
    }).done(function (response) {
        if (response.estado) {
            MensajeExitoso(`Parámetro '${param.Codigo}' actualizado`);
        } else {
            MensajeGrowlSwal(response.mensaje);
        }
    }).fail(function (error) {
        MsgAjaxError(error);
    })

    console.log(param);
}
