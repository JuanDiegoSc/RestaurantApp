namespace Restaurant.Web.Models.EmployeeModels
{
    using System;

    public class HorarioViewModel
    {
        public int Id { get; set; }
        public string DiaSemana { get; set; }
        public string Horas { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

}
