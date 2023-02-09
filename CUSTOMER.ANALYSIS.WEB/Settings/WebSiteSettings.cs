namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Settings
{
    internal sealed class WebSiteSettings
    {
        public DBSettings ConnectionCredentials { get; set; }
        public SitioWebSettings SITIOWEB { get; set; }
        public IOSettings IO { get; set; }
        public LogSettings LOG { get; set; }
        //public RedisSettings REDIS { get; set; }

        public Reporte FormatoReporte { get; set; }

        internal sealed class Reporte
        {
            public string RutaBase { get; set; }
            public string NombreArchivo { get; set; }
            public string Extension { get; set; }
        }

        internal sealed class DBSettings
        {
            public string DataSource { get; set; }
            public string InitialCatalog { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
        }
        internal sealed class SitioWebSettings
        {
            public string TimeZoneId { get; set; }
            public string NitCiaNube { get; set; }
            public string Footer { get; set; }
            public string LimiteConsulta { get; set; }
            public short LimiteSesion { get; set; }
            public ReCaptchaSettings Recaptcha { get; set; }
        }
        internal sealed class ReCaptchaSettings
        {
            public string ClaveSitioWeb { get; set; }
            public string ClaveComGoogle { get; set; }
        }
        internal sealed class IOSettings
        {
            public short Destino { get; set; }
            public IOAzureBlobSettings AzureBlob { get; set; }
        }

        internal sealed class IOAzureBlobSettings
        {
            public string ConnectionString { get; set; }
        }

        internal sealed class LogSettings
        {
            public bool Habilitar { get; set; }
            public short Destino { get; set; }
            public LogDiskSettings Disk { get; set; }
            public SqlServerSettings SqlServer { get; set; }
            public LogAzureSettings AzureLog { get; set; }
        }
        internal sealed class LogDiskSettings
        {
            public string Ruta { get; set; }
            public string Layout { get; set; }
            public string FileName { get; set; }
        }
        internal sealed class SqlServerSettings
        {
            public string DataSource { get; set; }
            public string InitialCatalog { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
        }

        internal sealed class LogAzureSettings
        {
            public string ConnectionString { get; set; }
            public string ContainerName { get; set; }
        }

        internal sealed class RedisSettings
        {
            public short Destino { get; set; }
            public RedisAzureSettings AzureRedis { get; set; }
        }

        internal sealed class RedisAzureSettings
        {
            public string ConnectionString { get; set; }
        }
    }
}
