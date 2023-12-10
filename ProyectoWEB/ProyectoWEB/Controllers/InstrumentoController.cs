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
        //[FiltroSeguridad]
        public IActionResult ConsultarPlatillos()
        {
            var datos = _instrumentoModel.ConsultarPlatillos();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarBaterias()
        {
            var datos = _instrumentoModel.ConsultarBaterias();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarBajos()
        {
            var datos = _instrumentoModel.ConsultarBajos();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarGuitarras()
        {
            var datos = _instrumentoModel.ConsultarGuitarras();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarPianos()
        {
            var datos = _instrumentoModel.ConsultarPianos();
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
