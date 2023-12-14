using Microsoft.AspNetCore.Mvc;
using ProyectoWEB.Entities;
using ProyectoWEB.Models;

namespace ProyectoWEB.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class LoginController : Controller
    {
  
        private readonly IUsuarioModel _usuarioModel;
        private readonly IInstrumentoModel _instrumentoModel;
        private readonly ICarritoModel _carritoModel;


        public LoginController(IUsuarioModel usuarioModel, IInstrumentoModel instrumentoModel, ICarritoModel carritoModel)
        {
            _usuarioModel = usuarioModel;
            _instrumentoModel = instrumentoModel;
            _carritoModel = carritoModel;

        }
    

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult IniciarSesion(UsuarioEnt entidad)
        {
            if (!ModelState.IsValid)
                return View();

            var resp = _usuarioModel.IniciarSesion(entidad);
            if (resp != null)
            {
                HttpContext.Session.SetString("NombreUsuario", resp.Nombre);
                HttpContext.Session.SetString("TokenUsuario", resp.Token);
                HttpContext.Session.SetString("RolUsuario", resp.IdRol.ToString());

                var datos = _carritoModel.ConsultarCarrito();
                HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
                HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());
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
            entidad.IdUsuario = long.Parse(q);
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
}

