using Microsoft.AspNetCore.Mvc;

namespace ProyectoWEB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

		public IActionResult RegistrarCuenta()
		{
			return View();
		}
	}
}

