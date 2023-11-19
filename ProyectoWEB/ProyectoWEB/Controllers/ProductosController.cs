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


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            var datos = _productosModel.ConsultarProductos();
            return View(datos);
        }
    }
}
