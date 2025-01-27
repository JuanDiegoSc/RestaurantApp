using System.ComponentModel.DataAnnotations;

namespace Restaurant.Web.Models.EmployeeModels
{
    public class CrearHorarioViewModel
    {
        [Required]
        public int EmpleadoId { get; set; }
        [Required]
        public string DiaSemana { get; set; }
        [Required]
        public string Horas { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
    }
}
