var urlBaseHub = ""

scanButton.addEventListener("click", async () => {

    if (!("NDEFReader" in window)) {
        MensajeGrowl("No es compatible con NFC");
        return;
    }

    $("#scanButton").text("Escaneando...");

    try {
        const ndef = new NDEFReader();
        await ndef.scan();
        console.log("> Scan started");

        ndef.addEventListener("readingerror", () => {
            MensajeGrowl("¡Argh! No se pueden leer los datos de la etiqueta NFC. ¿Prueba otro?");
        });

        ndef.addEventListener("reading", ({ message, serialNumber }) => {
            console.log(`> Serial Number: ${serialNumber}`);
            console.log(`> Records: (${message.records.length})`);
            $("#scanButton").text("Escanear Arete");
            buscarAnimal(serialNumber);
        });
    } catch (error) {
        console.log("Argh! " + error);
    }
});

const buscarAnimal = (codigo) => {
    $.post(urlBaseHub + "Animal/BuscarAnimalPorCodigo", { codigo, Encriptado: true }).done(function (response) {
        if (response.estado) {
            MensajeExitosoSwalConfirmacionRedireccion(`Arete Escaneado [${codigo}]`, "Animal", urlBaseHub + "HistorialClinico/DetalleV2/?Id=" + response.data);
        } else {
            MensajeGrowl(`El código escaneado no se encuentra registrado: [${codigo}]`);
        }
    }).fail(function (error) {
        MsgAjaxError(error);
    })
}
