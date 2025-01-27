namespace Restaurant.Web.Models.EmployeeModels
{
    public class AdminCambiarEstadoSolicitudViewModel
    {
        public int Id { get; set; }
        public string Estado { get; set; } // Valores posibles: "Aprobado", "Rechazado"
    }

}
