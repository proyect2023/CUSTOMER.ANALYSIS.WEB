namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices
{
    public class UsuarioInternoQueryDto
    {
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public long IdCompania { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public long IdRol { get; set; }
        public string Rol { get; set; }
    }
}
