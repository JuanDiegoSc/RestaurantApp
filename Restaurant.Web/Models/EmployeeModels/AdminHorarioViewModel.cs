namespace Restaurant.Web.Models.EmployeeModels
{
    public class AdminHorarioViewModel
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DiaSemana { get; set; }
        public string Horas { get; set; }
    }
}
