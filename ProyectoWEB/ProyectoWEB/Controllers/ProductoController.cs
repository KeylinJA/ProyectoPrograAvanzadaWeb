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
    }
}
