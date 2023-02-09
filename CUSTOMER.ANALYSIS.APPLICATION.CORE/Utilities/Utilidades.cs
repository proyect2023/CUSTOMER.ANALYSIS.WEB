using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using GS.TOOLS;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities
{
    public static class Utilidades
    {
        public static string OcultarCaracteres(string text, int count)
        {
            string TextoEnx = "";
            for (int i = 0; i < count; i++)
            {
                TextoEnx = TextoEnx + "X";
            }

            int tamanoString = text.Length;
            string SubStringCorreosInicial = text.Substring(count, tamanoString - count);
            return TextoEnx + SubStringCorreosInicial;
        }

        public static bool ValidarClave(string ClaveActual, string ClaveActualHash, string ClaveNueva, string ClaveNuevaConfirma, bool EsCorreoRecuperacion, ref string mensaje)
        {
            if (!GSCrypto.ConfirmHashV1(ClaveActual, ClaveActualHash))
            {
                if (EsCorreoRecuperacion)
                    mensaje = "La contraseña temporal es incorrecta. Verifique su correo";
                else
                    mensaje = "La contraseña actual es incorrecta";
                return false;
            }

            if (ClaveNueva.Length < 8)
            {
                mensaje = "La nueva contraseña debe tener minimo 8 caracteres";
                return false;
            }

            string caracteresPermitidos = "._-*$#%&?!";
            int contMAYUS = 0;
            int contMINUS = 0;
            int contNUM = 0;
            int contESP = 0;

            foreach (char c in ClaveNueva)
            {
                if (!char.IsDigit(c) & !char.IsLetter(c) & !caracteresPermitidos.Contains(c))
                {
                    mensaje = "La nueva contraseña solo puede contener letras, números y los caracteres especiales: ._-*$#%&?!";
                    return false;
                }
                if (char.IsUpper(c))
                    contMAYUS += 1;
                if (char.IsLower(c))
                    contMINUS += 1;
                if (char.IsDigit(c))
                    contNUM += 1;
                if (caracteresPermitidos.Contains(c))
                    contESP += 1;
            }

            if (contMAYUS == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos una letra mayúscula";
                return false;
            }
            if (contMINUS == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos una letra minúscula";
                return false;
            }
            if (contNUM == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos un número";
                return false;
            }
            if (contESP == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos uno de los siguientes caracteres especiales: ._-*$#%&?!";
                return false;
            }

            if (ClaveNueva != ClaveNuevaConfirma)
            {
                mensaje = "La nueva contraseña no coincide con la confirmación";
                return false;
            }

            if (GSCrypto.ConfirmHashV1(ClaveNueva, ClaveActualHash))
            {
                if (EsCorreoRecuperacion)
                    mensaje = "La nueva contraseña no puede ser igual a la recibida por correo";
                else
                    mensaje = "La nueva contraseña no puede ser igual a la anterior";
                return false;
            }

            return true;
        }

        public static DateTime GetHoraActual(string TimeZone = "")
        {
            if (string.IsNullOrEmpty(TimeZone))
                TimeZone = GlobalSettings.TimeZoneId;

            if (string.IsNullOrEmpty(TimeZone))
            {
                return DateTime.Now;
            }
            else
            {
                try
                {
                    return ConvertTimeZone(DateTime.UtcNow, TimeZone);
                }
                catch (Exception)
                {
                    return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(TimeZone));
                }
            }
        }

        public static DateTime ConvertTimeZone(DateTime dateTime, string timeInfo)
        {
            var result = OlsonTimeZoneToTimeZoneInfo(timeInfo);
            if (result != null)
            {
                var finalTimeZone = TimeZoneInfo.FindSystemTimeZoneById(result);
                dateTime = (dateTime != null ? TimeZoneInfo.ConvertTimeFromUtc(dateTime != null ? dateTime : DateTime.UtcNow, finalTimeZone) : DateTime.UtcNow);
            }
            return dateTime;
        }

        public static string OlsonTimeZoneToTimeZoneInfo(string timeInfo)
        {
            var olsonWindowsTimes = new Dictionary<string, string>()
            {
                { "Africa/Bangui", "W. Central Africa Standard Time" },
                { "Africa/Cairo", "Egypt Standard Time" },
                { "Africa/Casablanca", "Morocco Standard Time" },
                { "Africa/Harare", "South Africa Standard Time" },
                { "Africa/Johannesburg", "South Africa Standard Time" },
                { "Africa/Lagos", "W. Central Africa Standard Time" },
                { "Africa/Monrovia", "Greenwich Standard Time" },
                { "Africa/Nairobi", "E. Africa Standard Time" },
                { "Africa/Windhoek", "Namibia Standard Time" },
                { "America/Anchorage", "Alaskan Standard Time" },
                { "America/Argentina/San_Juan", "Argentina Standard Time" },
                { "America/Asuncion", "Paraguay Standard Time" },
                { "America/Bahia", "Bahia Standard Time" },
                { "America/Bogota", "SA Pacific Standard Time" },
                { "America/Buenos_Aires", "Argentina Standard Time" },
                { "America/Caracas", "Venezuela Standard Time" },
                { "America/Cayenne", "SA Eastern Standard Time" },
                { "America/Chicago", "Central Standard Time" },
                { "America/Chihuahua", "Mountain Standard Time (Mexico)" },
                { "America/Cuiaba", "Central Brazilian Standard Time" },
                { "America/Denver", "Mountain Standard Time" },
                { "America/Fortaleza", "SA Eastern Standard Time" },
                { "America/Godthab", "Greenland Standard Time" },
                { "America/Guatemala", "Central America Standard Time" },
                { "America/Halifax", "Atlantic Standard Time" },
                { "America/Indianapolis", "US Eastern Standard Time" },
                { "America/Indiana/Indianapolis", "US Eastern Standard Time" },
                { "America/La_Paz", "SA Western Standard Time" },
                { "America/Los_Angeles", "Pacific Standard Time" },
                { "America/Mexico_City", "Mexico Standard Time" },
                { "America/Montevideo", "Montevideo Standard Time" },
                { "America/New_York", "Eastern Standard Time" },
                { "America/Noronha", "UTC-02" },
                { "America/Phoenix", "US Mountain Standard Time" },
                { "America/Regina", "Canada Central Standard Time" },
                { "America/Santa_Isabel", "Pacific Standard Time (Mexico)" },
                { "America/Santiago", "Pacific SA Standard Time" },
                { "America/Sao_Paulo", "E. South America Standard Time" },
                { "America/St_Johns", "Newfoundland Standard Time" },
                { "America/Tijuana", "Pacific Standard Time" },
                { "Antarctica/McMurdo", "New Zealand Standard Time" },
                { "Atlantic/South_Georgia", "UTC-02" },
                { "Asia/Almaty", "Central Asia Standard Time" },
                { "Asia/Amman", "Jordan Standard Time" },
                { "Asia/Baghdad", "Arabic Standard Time" },
                { "Asia/Baku", "Azerbaijan Standard Time" },
                { "Asia/Bangkok", "SE Asia Standard Time" },
                { "Asia/Beirut", "Middle East Standard Time" },
                { "Asia/Calcutta", "India Standard Time" },
                { "Asia/Colombo", "Sri Lanka Standard Time" },
                { "Asia/Damascus", "Syria Standard Time" },
                { "Asia/Dhaka", "Bangladesh Standard Time" },
                { "Asia/Dubai", "Arabian Standard Time" },
                { "Asia/Irkutsk", "North Asia East Standard Time" },
                { "Asia/Jerusalem", "Israel Standard Time" },
                { "Asia/Kabul", "Afghanistan Standard Time" },
                { "Asia/Kamchatka", "Kamchatka Standard Time" },
                { "Asia/Karachi", "Pakistan Standard Time" },
                { "Asia/Katmandu", "Nepal Standard Time" },
                { "Asia/Kolkata", "India Standard Time" },
                { "Asia/Krasnoyarsk", "North Asia Standard Time" },
                { "Asia/Kuala_Lumpur", "Singapore Standard Time" },
                { "Asia/Kuwait", "Arab Standard Time" },
                { "Asia/Magadan", "Magadan Standard Time" },
                { "Asia/Muscat", "Arabian Standard Time" },
                { "Asia/Novosibirsk", "N. Central Asia Standard Time" },
                { "Asia/Oral", "West Asia Standard Time" },
                { "Asia/Rangoon", "Myanmar Standard Time" },
                { "Asia/Riyadh", "Arab Standard Time" },
                { "Asia/Seoul", "Korea Standard Time" },
                { "Asia/Shanghai", "China Standard Time" },
                { "Asia/Singapore", "Singapore Standard Time" },
                { "Asia/Taipei", "Taipei Standard Time" },
                { "Asia/Tashkent", "West Asia Standard Time" },
                { "Asia/Tbilisi", "Georgian Standard Time" },
                { "Asia/Tehran", "Iran Standard Time" },
                { "Asia/Tokyo", "Tokyo Standard Time" },
                { "Asia/Ulaanbaatar", "Ulaanbaatar Standard Time" },
                { "Asia/Vladivostok", "Vladivostok Standard Time" },
                { "Asia/Yakutsk", "Yakutsk Standard Time" },
                { "Asia/Yekaterinburg", "Ekaterinburg Standard Time" },
                { "Asia/Yerevan", "Armenian Standard Time" },
                { "Atlantic/Azores", "Azores Standard Time" },
                { "Atlantic/Cape_Verde", "Cape Verde Standard Time" },
                { "Atlantic/Reykjavik", "Greenwich Standard Time" },
                { "Australia/Adelaide", "Cen. Australia Standard Time" },
                { "Australia/Brisbane", "E. Australia Standard Time" },
                { "Australia/Darwin", "AUS Central Standard Time" },
                { "Australia/Hobart", "Tasmania Standard Time" },
                { "Australia/Perth", "W. Australia Standard Time" },
                { "Australia/Sydney", "AUS Eastern Standard Time" },
                { "Etc/GMT", "UTC" },
                { "Etc/GMT+11", "UTC-11" },
                { "Etc/GMT+12", "Dateline Standard Time" },
                { "Etc/GMT+2", "UTC-02" },
                { "Etc/GMT-12", "UTC+12" },
                { "Europe/Amsterdam", "W. Europe Standard Time" },
                { "Europe/Athens", "GTB Standard Time" },
                { "Europe/Belgrade", "Central Europe Standard Time" },
                { "Europe/Berlin", "W. Europe Standard Time" },
                { "Europe/Brussels", "Romance Standard Time" },
                { "Europe/Budapest", "Central Europe Standard Time" },
                { "Europe/Dublin", "GMT Standard Time" },
                { "Europe/Helsinki", "FLE Standard Time" },
                { "Europe/Istanbul", "GTB Standard Time" },
                { "Europe/Kiev", "FLE Standard Time" },
                { "Europe/London", "GMT Standard Time" },
                { "Europe/Minsk", "E. Europe Standard Time" },
                { "Europe/Moscow", "Russian Standard Time" },
                { "Europe/Paris", "Romance Standard Time" },
                { "Europe/Sarajevo", "Central European Standard Time" },
                { "Europe/Warsaw", "Central European Standard Time" },
                { "Indian/Mauritius", "Mauritius Standard Time" },
                { "Pacific/Apia", "Samoa Standard Time" },
                { "Pacific/Auckland", "New Zealand Standard Time" },
                { "Pacific/Fiji", "Fiji Standard Time" },
                { "Pacific/Guadalcanal", "Central Pacific Standard Time" },
                { "Pacific/Guam", "West Pacific Standard Time" },
                { "Pacific/Honolulu", "Hawaiian Standard Time" },
                { "Pacific/Pago_Pago", "UTC-11" },
                { "Pacific/Port_Moresby", "West Pacific Standard Time" },
                { "Pacific/Tongatapu", "Tonga Standard Time" }
            };

            string timeInfoKey = string.Empty;
            if (olsonWindowsTimes.ContainsValue(timeInfo))
                timeInfoKey = olsonWindowsTimes.FirstOrDefault(x => x.Value == timeInfo).Key;

            return timeInfoKey;
        }

        public static string FormatearFechaFormato(DateTime? fecha, string formato = "dd/MM/yyyy")
        {
            if (fecha == null) return "--";
            return fecha.Value.ToString(formato);
        }

        public static string ConcatenarValores(this string[] valores)
        {
            if (valores is null) return "";

            int cont = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var item in valores)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (cont == 0)
                    {
                        sb.Append(item);
                    }
                    else
                    {
                        sb.Append(";");
                        sb.Append(item);
                    }
                    cont++;
                }
            }

            return sb.ToString();
        }

        public static string DoubleToString_FrontCO(decimal? valor, int DigitoDecimal)
        {
            if (valor is null)
            {
                valor = 0;
            }
            string resp = string.Empty;
            try
            {
                resp = Strings.FormatNumber(valor, DigitoDecimal);
                if ((System.Convert.ToDecimal(123.456).ToString()).Contains(","))
                {
                    resp = resp.Replace(".", "|");
                    resp = resp.Replace(",", ".");
                    resp = resp.Replace("|", ",");
                }
            }
            catch (Exception ex)
            {
            }
            return resp;
        }

        public static string DepuraStrConvertNum(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                cadena = "0";
            if (cadena == "undefined")
                cadena = "0";
            cadena = cadena.Replace("$", "").Replace(" ", "").Replace("%", "").Replace(",", "").Replace(".", ",");
            if (cadena.Trim() == "")
                cadena = "0";
            return cadena;
        }

        public static string RandomString(int longitudContrasena = 10)
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890.!$@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = longitudContrasena;
            StringBuilder contraseniaAleatoria = new StringBuilder();
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria.Append(letra.ToString());
            }

            return contraseniaAleatoria.ToString();
        }

        public static void SetCultureInfo()
        {
            CultureInfo forceDotCulture;
            forceDotCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            forceDotCulture.NumberFormat.CurrencySymbol = "$";
            forceDotCulture.NumberFormat.NumberDecimalSeparator = ",";
            forceDotCulture.NumberFormat.CurrencyDecimalSeparator = ",";
            forceDotCulture.NumberFormat.NumberGroupSeparator = ".";
            forceDotCulture.NumberFormat.CurrencyGroupSeparator = ".";
            forceDotCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = forceDotCulture;
            CultureInfo.DefaultThreadCurrentUICulture = forceDotCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = forceDotCulture;
        }

    }
}
