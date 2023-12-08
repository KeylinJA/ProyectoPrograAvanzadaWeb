using Microsoft.AspNetCore.Mvc;
using ProyectoWEB.Entities;
using ProyectoWEB.Models;

namespace ProyectoWEB.Controllers
{
    public class InstrumentoController : Controller
    {
        private readonly IInstrumentoModel _instrumentoModel;

        public InstrumentoController(IInstrumentoModel instrumentoModel)
        {
            _instrumentoModel = instrumentoModel;
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarInstrumentos()
        {
            var datos = _instrumentoModel.ConsultarInstrumentos();
            return View(datos);
        }

        [HttpGet]
        public IActionResult ActualizarEstadoInstrumento(long q)
        {
            var entidad = new InstrumentoEnt();
            entidad.IdInstrumento = q;

            _instrumentoModel.ActualizarEstadoInstrumento(entidad);
            return RedirectToAction("ConsultarInstrumentos", "Instrumento");
        }
    } 
}
