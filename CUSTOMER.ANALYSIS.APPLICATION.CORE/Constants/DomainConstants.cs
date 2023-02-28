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
        public const string MENSAJE_CAMPO_MIN_LENGTH = "La cantidad de caracteres no es suficiente";
        public const string MENSAJE_CAMPO_EMAIL_ADDRESS = "El correo electrónico ingresado no es correcto";

        public const string COMPONENTE_NAME = "ANALISIS CLIENTE INTERCOM";

        public const string ENCRIPTA_KEY = "-L4gRa(3*.!,";

        public const string ERROR_CLIENTE_ANONIMO = "P101";
        public const string ERROR_CLIENTE_REGISTRADO_IDENTIFICACION = "P102";

        public const string ERROR_PLAN_REGISTRADO = "P201";
        public const string ERROR_PLAN_ANONIMO = "P202";

        public const string ERROR_USUARIO_REGISTRADO_CEDULA = "P501";
        public const string ERROR_USUARIO_REGISTRADO_MAIL = "P502";
        public const string ERROR_USUARIO_REGISTRADO_USERNAME = "P503";
        public const string ERROR_USUARIO_NO_REGISTRADO = "P504";
        public const string ERROR_USUARIO_ANONIMO = "P505";


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

            ERROR_PLAN_REGISTRADO => $"{CodigoError}: El plan ya se encuentra registrado",
            ERROR_PLAN_ANONIMO => $"{CodigoError}: El plan no se encuentra registrado",
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
