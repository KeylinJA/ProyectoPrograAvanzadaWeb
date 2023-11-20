using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWEB.Controllers;
using ProyectoWEB.Models;

namespace PlantillaProyecto.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoModel _productosModel;

        public ProductosController(IProductoModel productoModel)
        {
            _productosModel = productoModel;
        }


        [HttpGet]
        public IActionResult InicioProductos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            var datos = _productosModel.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public IActionResult Platillos()
        {
            return View();
        }

    }
}
