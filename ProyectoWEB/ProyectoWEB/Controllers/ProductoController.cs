using Microsoft.AspNetCore.Mvc;
using ProyectoWEB.Models;

namespace ProyectoWEB.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class ProductoController : Controller
    {
        private readonly IProductoModel _productoModel;

        public ProductoController (IProductoModel productoModel)
        {
            _productoModel = productoModel;
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarProductos()
        {
            var datos = _productoModel.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Carrito()
        {
            var datos = _productoModel.Carrito();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Detalle()
        {
            var datos = _productoModel.Detalle();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Platillos()
        {
            var datos = _productoModel.Platillos();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Baterias()
        {
            var datos = _productoModel.Baterias();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Bajos()
        {
            var datos = _productoModel.Bajos();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Guitarras()
        {
            var datos = _productoModel.Guitarras();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult AudioVideo()
        {
            var datos = _productoModel.AudioVideo();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Viento()
        {
            var datos = _productoModel.Viento();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult PianosTeclados()
        {
            var datos = _productoModel.PianosTeclados();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult Bases()
        {
            var datos = _productoModel.Bases();
            return View(datos);
        }
    }
}
