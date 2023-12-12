using Microsoft.AspNetCore.Mvc;
using ProyectoWEB.Models;

namespace ProyectoWEB.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;
        public UsuarioController(IUsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
        }

        [HttpGet]
        public IActionResult PerfilUsuario()
        {
            var datos = _usuarioModel.ConsultarUsuario();
            return View(datos);
        }
    }
}
