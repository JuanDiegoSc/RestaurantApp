namespace Restaurant.Web.Models.EmployeeModels
{
    public class AdminEmpleadoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public List<AdminHorarioViewModel> Horarios { get; set; }
        public List<AdminSolicitudVacacionesViewModel> SolicitudesVacaciones { get; set; }
    }


}
