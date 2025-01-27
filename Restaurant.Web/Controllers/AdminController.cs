using Restaurant.Web.Models.EmployeeModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurant.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Vista principal del panel de administración
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7293/api/empleado");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = "No se pudo obtener la lista de empleados.";
                return View(new List<AdminEmpleadoViewModel>()); // Inicializa con una lista vacía
            }

            var empleados = await response.Content.ReadFromJsonAsync<List<AdminEmpleadoViewModel>>();
            return View(empleados);
        }

        // Crear un nuevo empleado (GET: formulario)
        [HttpGet]
        public IActionResult CrearEmpleado()
        {
            return View(new AdminEmpleadoViewModel());
        }

        // Crear un nuevo empleado (POST: enviar a la API)
        [HttpPost]
        public async Task<IActionResult> CrearEmpleado(AdminEmpleadoViewModel empleado)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Crear un objeto simplificado con los datos necesarios
            var payload = new
            {
                nombre = empleado.Nombre,
                usuario = empleado.Usuario,
                contraseña = empleado.Contraseña
            };

            // Enviar el payload a la API
            var response = await httpClient.PostAsJsonAsync("https://localhost:7293/api/empleado", payload);

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = "No se pudo crear el empleado. Verifica los datos e inténtalo nuevamente.";
                return View(empleado);
            }

            return RedirectToAction("Index");
        }

        // Editar un empleado (GET: formulario)
        [HttpGet]
        public async Task<IActionResult> EditarEmpleado(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7293/api/empleado/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo cargar el empleado.";
                return RedirectToAction("Index");
            }

            var empleado = await response.Content.ReadFromJsonAsync<EditarEmpleadoViewModel>();
            return View(empleado);
        }

        // Editar un empleado (POST: enviar a la API)
        [HttpPost]
        public async Task<IActionResult> EditarEmpleado(EditarEmpleadoViewModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var payload = new
            {
                id = model.Id,
                nombre = model.Nombre,
                usuario = model.Usuario,
                contraseña = model.Contraseña // Opcional
            };

            var response = await httpClient.PutAsJsonAsync($"https://localhost:7293/api/empleado/{model.Id}", payload);

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "Empleado editado correctamente.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "No se pudo editar el empleado. Intente nuevamente.";
            return View(model);
        }

        // Eliminar un empleado
        [HttpPost]
        public async Task<IActionResult> EliminarEmpleado(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7293/api/empleado/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = $"El empleado con ID {id} fue eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = $"No se pudo eliminar el empleado con ID {id}. Intenta nuevamente.";
            }

            return RedirectToAction("Index");
        }

        // Ver solicitudes de vacaciones
        [HttpGet]
        public async Task<IActionResult> SolicitudesVacaciones(int empleadoId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7293/api/SolicitudVacaciones/empleado/{empleadoId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = "No se pudieron obtener las solicitudes de vacaciones.";
                return View(new List<AdminSolicitudVacacionesViewModel>()); // Retorna una lista vacía para evitar errores
            }

            var solicitudes = await response.Content.ReadFromJsonAsync<List<AdminSolicitudVacacionesViewModel>>();
            return View(solicitudes);
        }

        // Cambiar estado de solicitud de vacaciones
        [HttpPost]
        public async Task<IActionResult> CambiarEstadoSolicitud(int id, string estado)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PutAsJsonAsync($"https://localhost:7293/api/SolicitudVacaciones/{id}/estado", estado);

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = $"La solicitud con ID {id} ha sido {estado.ToLower()} con éxito.";
            }
            else
            {
                TempData["Error"] = $"No se pudo cambiar el estado de la solicitud con ID {id}.";
            }

            return RedirectToAction("SolicitudesVacaciones");
        }

        // Buscar un empleado por ID
        [HttpGet]
        public async Task<IActionResult> BuscarEmpleado(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7293/api/empleado/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo encontrar el empleado.";
                return RedirectToAction("Index"); // Redirige al Index si no encuentra el empleado
            }

            // Obtener datos del empleado desde la API
            var empleado = await response.Content.ReadFromJsonAsync<AdminEmpleadoViewModel>();
            return View("DetalleEmpleado", empleado); // Renderiza la vista "DetalleEmpleado" con los datos
        }

        [HttpGet]
        public async Task<IActionResult> ReporteComparativo(int empleadoId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7293/api/reporte/{empleadoId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = "No se pudo generar el reporte.";
                return RedirectToAction("Index");
            }

            var reporte = await response.Content.ReadFromJsonAsync<List<AdminReporteComparativoViewModel>>();
            return View(reporte); // Asegúrate de que esta línea pase el modelo correcto
        }

        [HttpGet]
        public IActionResult CrearHorario(int? empleadoId)
        {
            var model = new CrearHorarioViewModel();

            // Si se pasa el ID del empleado, lo prellenamos
            if (empleadoId.HasValue)
            {
                model.EmpleadoId = empleadoId.Value;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CrearHorario(CrearHorarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var httpClient = _httpClientFactory.CreateClient();

            // Crear el objeto que espera el API
            var horario = new
            {
                model.EmpleadoId,
                model.FechaInicio,
                model.FechaFin,
                model.DiaSemana,
                model.Horas
            };

            // Enviar al API
            var response = await httpClient.PostAsJsonAsync("https://localhost:7293/api/Horario", horario);

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "Horario asignado con éxito.";
                return RedirectToAction("Index");
            }

            ViewData["Error"] = "No se pudo asignar el horario. Intente nuevamente.";
            return View(model);
        }
    }

}
