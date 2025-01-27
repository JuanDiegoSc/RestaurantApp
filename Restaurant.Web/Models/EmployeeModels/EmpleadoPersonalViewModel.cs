using System.Collections.Generic;

namespace Restaurant.Web.Models.EmployeeModels
{
    public class EmpleadoPersonalViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<HorarioViewModel> Horarios { get; set; } = new List<HorarioViewModel>();
        public List<SolicitudVacacionesViewModel> SolicitudesVacaciones { get; set; } = new List<SolicitudVacacionesViewModel>();
    }

}
