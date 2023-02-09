using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants
{
    public static class DomainConstants
    {
        public const string CODIGO_LECHE = "CODIGO_LECHE_GLOBAL";
        public const string CODIGO_TRANSACCION = "{Fecha}-{Usuario}-{Compania}-{Tipo_Inventario}-{Tipo_Movimiento}-{Tabla}-{Id_Inventario}-{Hora}";

        public const string MENSAJE_CAMPO_REQUIRED = "Este campo es requerido";
        public const string MENSAJE_CAMPO_MAX_LENGTH = "Ha excedido la capacidad máxima de caracteres";
        public const string MENSAJE_CAMPO_EMAIL_ADDRESS = "El correo electrónico ingresado no es correcto";

        public const string COMPONENTE_NAME = "ANALISIS CLIENTE INTERCOM";

        public const string ENCRIPTA_KEY = "-L4gRa(3*.!,";

        public const string ERROR_CLIENTE_ANONIMO = "P101";
        public const string ERROR_CLIENTE_REGISTRADO_IDENTIFICACION = "P102";

        public const string ERROR_PROVEEDOR_ANONIMO = "P201";
        public const string ERROR_PROVEEDOR_REGISTRADO_IDENTIFICACION = "P202";

        public const string ERROR_CATALOGO_ANONIMO = "P301";
        public const string ERROR_CATALOGO_REGISTRADO_DESCRIPCION = "P302";
        public const string ERROR_CATALOGO_REGISTRADO_DESCRIPCION2 = "P303";
        public const string ERROR_CATALOGO_REGISTRADO_DESCRIPCION3 = "P304";

        public const string ERROR_ANIMAL_ANONIMO = "P401";
        public const string ERROR_ANIMAL_REGISTRADO_CODIGO = "P402";

        public const string ERROR_USUARIO_REGISTRADO_CEDULA = "P501";
        public const string ERROR_USUARIO_REGISTRADO_MAIL = "P502";
        public const string ERROR_USUARIO_REGISTRADO_USERNAME = "P503";
        public const string ERROR_USUARIO_NO_REGISTRADO = "P504";
        public const string ERROR_USUARIO_ANONIMO = "P505";

        public const string ERROR_PRODUCTO_ANONIMO = "P601";
        public const string ERROR_PRODUCTO_REGISTRADO_CODIGO = "P602";
        public const string ERROR_PRODUCTO_REGISTRADO_BODEGA = "P603";

        public const string ERROR_PALPACION_ANONIMO = "P701";
        //public const string ERROR_PALPACION_REGISTRADO_CODIGO = "P702";

        public const string ERROR_GESTACION_ANONIMO = "P801";
        //public const string ERROR_GESTACION_REGISTRADO_CODIGO = "P802";

        public const string ERROR_INSEMINACION_ANONIMO = "P901";
        //public const string ERROR_INSEMINACION_REGISTRADO_CODIGO = "P402";

        public const string ERROR_PRODUCCION_ANONIMO = "P1001";
        public const string ERROR_PESAJE_ANONIMO = "P1101";

        public const string ERROR_CONTROL_SANITARIO_ANONIMA = "P1201";
        public const string ERROR_CONTROL_SANITARIO_DETALLE = "P1202";
        public const string ERROR_CONTROL_SANITARIO_ANIMAL = "P1203";
        public const string ERROR_CONTROL_SANITARIO_CONTROL = "P1204";
        public const string ERROR_CONTROL_SANITARIO_ENFERMEDAD = "P1205";
        public const string ERROR_CONTROL_SANITARIO_FECHA_CONTROL = "P1206";
        public const string ERROR_CONTROL_SANITARIO_FECHA_CONTROL_PROXIMO = "P1207";

        public const string ERROR_CRIA_ANONIMA = "P1301";
        public const string ERROR_CRIA_CODIGO = "P1302";

        public const string ERROR_FACTURA_ANONIMA = "P1401";
        public const string ERROR_FACTURA_DETALLE = "P1402";
        public const string ERROR_FACTURA_FORMA_PAGO = "P1403";
        public const string ERROR_FACTURA_MONTO_PAGAR = "P1404";
        public const string ERROR_FACTURA_SECUENCIAL = "P1405";
        public const string ERROR_FACTURA_NUMERO_DOCUMENTO = "P1406";
        public const string ERROR_FACTURA_REGISTRADA = "P1407";


        public const string ERROR_ACTIVIDAD_FECHA_ACTIVIDAD = "P1501";
        public const string ERROR_ACTIVIDAD_DETALLE = "P1502";
        public const string ERROR_ACTIVIDAD_ANONIMA = "P1503";
        public const string ERROR_ACTIVIDAD_DIAS_TRATAMIENTO = "P1504";


        public const string ERROR_ENVIAR_MAIL = "P018";

        public const string MAIL_BIENVENIDA_ASUNTO = "MAIL_BIENVENIDA_ASUNTO";
        public const string MAIL_BIENVENIDA_CUERPO = "MAIL_BIENVENIDA_CUERPO";

        public const string MAIL_RECUPERAR_PASSWORD_ASUNTO = "MAIL_RECUPERAR_PASSWORD_ASUNTO";
        public const string MAIL_RECUPERAR_PASSWORD_CUERPO = "MAIL_RECUPERAR_PASSWORD_CUERPO";

        public const string PARAM_CUERPO = "{Cuerpo}";
        public const string PARAM_USERNAME = "{Username}";
        public const string PARAM_PASSWORD_TEMPORAL = "{Password_Temporal}";
        public const string PARAM_USUARIO_NOMBRE_COMPLETO = "{Usuario_Nombre_Completo}";



        public const string ERROR_GENERAL = "E999";

        public static string ObtenerDescripcionError(string CodigoError) => CodigoError switch
        {
            ERROR_GENERAL => $"{CodigoError}: Ha ocurrido un error inesperado, por favor intenta nuevamente en unos minutos y si el error persiste comunícate con el área de soporte. ",
            ERROR_USUARIO_REGISTRADO_CEDULA => $"{CodigoError}: El número de cédula ya se encuentra registrado.",
            ERROR_USUARIO_REGISTRADO_MAIL => $"{CodigoError}: El correo electrónico ya se encuentra registrado.",
            ERROR_USUARIO_REGISTRADO_USERNAME => $"{CodigoError}: El usuario ya se encuentra registrado.",
            ERROR_USUARIO_NO_REGISTRADO => $"{CodigoError}: El usuario no se encuentra registrado.",
            ERROR_USUARIO_ANONIMO => $"{CodigoError}: El usuario o el correo electrónico no encontrado.",
            ERROR_ENVIAR_MAIL => $"{CodigoError}: Error al enviar el mail.",

            ERROR_CLIENTE_ANONIMO => $"{CodigoError}: El cliente no se encuetra registrado.",
            ERROR_CLIENTE_REGISTRADO_IDENTIFICACION => $"{CodigoError}: La identificación del cliente ya se encuentra registrada.",

            ERROR_PROVEEDOR_ANONIMO => $"{CodigoError}: El proveedor no se encuetra registrado.",
            ERROR_PROVEEDOR_REGISTRADO_IDENTIFICACION => $"{CodigoError}: La identificación del proveedor ya se encuentra registrada.",

            ERROR_CATALOGO_ANONIMO => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_CATALOGO_REGISTRADO_DESCRIPCION => $"{CodigoError}: La descripción ya se encuentra registrada.",
            ERROR_CATALOGO_REGISTRADO_DESCRIPCION2 => $"{CodigoError}: El valor ya se encuentra registrado.",
            ERROR_CATALOGO_REGISTRADO_DESCRIPCION3 => $"{CodigoError}: El registro ya se encuentra registrado con la misma fecha.",

            ERROR_PALPACION_ANONIMO => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_GESTACION_ANONIMO => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_INSEMINACION_ANONIMO => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_PRODUCCION_ANONIMO => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_PESAJE_ANONIMO => $"{CodigoError}: El valor no se encuetra registrado.",

            ERROR_CONTROL_SANITARIO_ANONIMA => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_CONTROL_SANITARIO_DETALLE => $"{CodigoError}: El control sanitario no contiene detalle.",
            ERROR_CONTROL_SANITARIO_ANIMAL => $"{CodigoError}: El animal seleccionado no se encuentra registrado.",
            ERROR_CONTROL_SANITARIO_CONTROL => $"{CodigoError}: El tipo de control no se encuentra registrado.",
            ERROR_CONTROL_SANITARIO_ENFERMEDAD => $"{CodigoError}: La enfermedad no se encuentra registrada.",
            ERROR_CONTROL_SANITARIO_FECHA_CONTROL => $"{CodigoError}: La fecha de control no es válida.",
            ERROR_CONTROL_SANITARIO_FECHA_CONTROL_PROXIMO => $"{CodigoError}: La fecha de control próxima no es válida.",

            ERROR_FACTURA_ANONIMA => $"{CodigoError}: La factura no se encuestra registrada.",
            ERROR_FACTURA_DETALLE => $"{CodigoError}: La factura no contiene detalle.",
            ERROR_FACTURA_FORMA_PAGO => $"{CodigoError}: La factura no contiene forma de pago.",
            ERROR_FACTURA_MONTO_PAGAR => $"{CodigoError}: La suma de las formas de pago es menor al valor total de la factura.",
            ERROR_FACTURA_SECUENCIAL => $"{CodigoError}: El secuencial es requerido para facturar.",
            ERROR_FACTURA_NUMERO_DOCUMENTO => $"{CodigoError}: El número de documento es requerido para facturar.",
            ERROR_FACTURA_REGISTRADA => $"{CodigoError}: El número de documento ya se encuentra registrado.",

            ERROR_ACTIVIDAD_ANONIMA => $"{CodigoError}: La actividad no no se encuetra registrado.",
            ERROR_ACTIVIDAD_DETALLE => $"{CodigoError}: La actividad no contiene detalle.",
            ERROR_ACTIVIDAD_FECHA_ACTIVIDAD => $"{CodigoError}: La fecha de actividad no es válida.",
            ERROR_ACTIVIDAD_DIAS_TRATAMIENTO => $"{CodigoError}: Los días de tratamiento no es válida.",

            ERROR_CRIA_ANONIMA => $"{CodigoError}: El valor no se encuetra registrado.",
            ERROR_CRIA_CODIGO => $"{CodigoError}: El código de la cría ya se encuentra registrado.",

            ERROR_ANIMAL_ANONIMO => $"{CodigoError}: El animal no se encuetra registrado.",
            ERROR_ANIMAL_REGISTRADO_CODIGO => $"{CodigoError}: El código de arete del animal ya se encuentra registrado.",

            ERROR_PRODUCTO_ANONIMO => $"{CodigoError}: El producto no se encuetra registrado.",
            ERROR_PRODUCTO_REGISTRADO_CODIGO => $"{CodigoError}: El código del producto ya se encuentra registrada.",
            ERROR_PRODUCTO_REGISTRADO_BODEGA => $"{CodigoError}: No se puede eliminar el producto, la producto se encuentra registrado en una bodega.",
        };
    }

    public enum EstadoUsuario
    {
        Eliminado = 0,
        Activo = 1,
        Bloqueado = 2,
        Inhabilitado = 3
    }

    public enum TipoProducto
    {
        Bien = 1,
        Servicio = 2
    }

    public enum EstadoFactura
    {
        Eliminado = 0,
        Facturado = 1,
        Borrador = 2,
        Proforma = 3,
        Todos = 99
    }
    public enum TipoInventario
    {
        proveedor = 1,
        venta = 2
    }

    public enum TipoMovimientoInventario
    {
        Todos = 0,
        Entrada = 1,
        Salida = 2
    }
    
    public enum SubtipoMovimientoInventario
    {
        Todos = 0,
        Manual = 1,
        //Tranferencia = 2,
        Factura = 3,
        [Display(Name = "Control Sanitario")]
        ControlSanitario = 4,
        [Display(Name = "Actividad de Manejo")]
        ActividadManejo = 5,
    }

}
