using System;

namespace Restaurant.Web.Models.EmployeeModels
{
    public class FormularioHorarioViewModel
    {
        public int EmpleadoId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Llenado { get; set; }
    }

}
