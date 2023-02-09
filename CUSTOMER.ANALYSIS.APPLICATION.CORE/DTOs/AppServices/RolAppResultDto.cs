namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class RolAppResultDto
    {
        public long IdRol { get; set; }
        public string Nombre { get; set; }
        //public string Descripcion { get; set; }
        public long IdCompania { get; set; }
        public bool Estado { get; set; }
    }
}
