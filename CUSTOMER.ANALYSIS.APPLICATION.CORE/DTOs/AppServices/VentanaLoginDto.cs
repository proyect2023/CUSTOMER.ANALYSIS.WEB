namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public sealed class VentanaLoginDto
    {
        public long IdPermiso { get; set; }
        public long Codigo { get; set; }
        public long? IdPadre { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public string NombreAbreviado { get; set; }
        public string Rol { get; set; }
        public long IdRol { get; set; }
        public int Orden { get; set; }
    }
}
