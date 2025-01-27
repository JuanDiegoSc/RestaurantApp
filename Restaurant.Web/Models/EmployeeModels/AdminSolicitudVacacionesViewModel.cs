namespace Restaurant.Web.Models.EmployeeModels
{
    public class AdminSolicitudVacacionesViewModel
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } // Pendiente, Aprobado, Rechazado
    }
}
