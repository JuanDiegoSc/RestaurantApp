using Restaurant.Web.Models.EmployeeModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurant.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpleadoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Página personal del empleado
        [HttpGet]
        public async Task<IActionResult> MiPaginaPersonal()
        {
            // Obtener el ID del empleado desde TempData
            if (!TempData.ContainsKey("EmpleadoId"))
            {
                return RedirectToAction("Index", "Login");
            }

            int empleadoId = (int)TempData["EmpleadoId"];
            TempData.Keep("EmpleadoId");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7293/api/empleado/{empleadoId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Error"] = "No se pudo cargar la información del empleado.";
                return View();
            }

            var empleado = await response.Content.ReadFromJsonAsync<EmpleadoPersonalViewModel>();
            return View(empleado);
        }

        // Llenar formulario de horario
        [HttpPost]
        public async Task<IActionResult> LlenarFormulario(FormularioHorarioViewModel formulario)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var payload = new
            {
                formulario.EmpleadoId,
                formulario.Fecha,
                formulario.Llenado
            };

            var response = await httpClient.PostAsJsonAsync("https://localhost:7293/api/FormularioHorario", payload);

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "Formulario llenado correctamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo llenar el formulario. Intenta nuevamente.";
            }

            return RedirectToAction("MiPaginaPersonal");
        }

        // Solicitar vacaciones
        [HttpPost]
        public async Task<IActionResult> SolicitarVacaciones(SolicitudVacacionesViewModel solicitud)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var payload = new
            {
                solicitud.EmpleadoId,
                solicitud.FechaSolicitud,
                solicitud.FechaInicio,
                solicitud.FechaFin
            };

            var response = await httpClient.PostAsJsonAsync("https://localhost:7293/api/SolicitudVacaciones", payload);

            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "Solicitud de vacaciones enviada correctamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo enviar la solicitud. Intenta nuevamente.";
            }

            return RedirectToAction("MiPaginaPersonal");
        }
    }

}
