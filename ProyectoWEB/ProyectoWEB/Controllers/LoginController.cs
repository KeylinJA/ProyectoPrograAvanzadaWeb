using Microsoft.AspNetCore.Mvc;

namespace ProyectoWEB.Controllers
{
    public class LoginController : Controller
    {
<<<<<<< Updated upstream
=======
        //Dependencias

        private readonly IUsuarioModel _usuarioModel;
        //

        //Constructores
        public LoginController(IUsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
        }
        //

        [HttpGet]
>>>>>>> Stashed changes
        public IActionResult Index()
        {
            return View();
        }

<<<<<<< Updated upstream
=======
        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
>>>>>>> Stashed changes
        public IActionResult IniciarSesion()
        {
            return View();
        }

<<<<<<< Updated upstream
		public IActionResult RegistrarCuenta()
		{
			return View();
		}
	}
=======
        [HttpPost]
        public IActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.IniciarSesion(entidad);
            if (resp != null)
            {
                HttpContext.Session.SetString("NombreUsuario", resp.Nombre);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo iniciar sesión";
                return View();
            }
        }

        [HttpGet]
        public IActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.RegistrarCuenta(entidad);
            if (resp == 1)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo registrar";
                return View();
            }

        }
        [HttpGet]
        public IActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.RecuperarCuenta(entidad);
            if (resp == 1)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo validar su cuenta";
                return View();
            }
        }

        [HttpGet]
        public IActionResult CambiarClaveCuenta(string q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuarioSeguro = q;
            return View(entidad);
        }

        [HttpPost]
        public IActionResult CambiarClaveCuenta(UsuarioEnt entidad)
        {
            var resp = _usuarioModel.CambiarClaveCuenta(entidad);
            if (resp == 1)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo actualizar su contraseña";
                return View();
            }
        }
    }
>>>>>>> Stashed changes
}

