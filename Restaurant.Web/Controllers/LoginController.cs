using Restaurant.Web.Models.EmployeeModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace Restaurant.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Mostrar la vista de login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Procesar el login
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            // Validar admin localmente
            if (loginViewModel.Usuario == "admin" && loginViewModel.Contrasena == "admin123")
            {
                TempData["Usuario"] = "Admin";
                TempData["Rol"] = "Admin";
                return RedirectToAction("Index", "Admin"); // Redirigir al panel de administración
            }

            // Consumir el endpoint de login de la API
            var httpClient = _httpClientFactory.CreateClient();
            var loginRequest = new
            {
                loginViewModel.Usuario,
                loginViewModel.Contrasena
            };

            var response = await httpClient.PostAsJsonAsync("https://localhost:7293/api/empleado/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                // Mostrar error si las credenciales son incorrectas
                ViewData["Error"] = "Usuario o contraseña incorrectos.";
                return View();
            }

            // Leer la respuesta del API
            var empleado = await response.Content.ReadFromJsonAsync<EmpleadoViewModel>();

            // Guardar los datos del empleado en TempData
            TempData["Usuario"] = empleado.Nombre;
            TempData["EmpleadoId"] = empleado.Id;
            TempData["Rol"] = "Empleado";

            // Redirigir a la página personal del empleado
            return RedirectToAction("MiPaginaPersonal", "Empleado");
        }
    }

}
