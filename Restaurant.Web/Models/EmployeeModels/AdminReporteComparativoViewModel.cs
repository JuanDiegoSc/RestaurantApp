namespace Restaurant.Web.Models.EmployeeModels
{
    public class AdminReporteComparativoViewModel
    {
        public int HorarioId { get; set; }
        public DateTime FechaInicioHorario { get; set; }
        public DateTime FechaFinHorario { get; set; }
        public string HorasAsignadas { get; set; }
        public bool LlenoFormulario { get; set; }
        public DateTime? FechaFormulario { get; set; }
        public string NombreEmpleado { get; set; }
        public string UsuarioEmpleado { get; set; }
    }
}
