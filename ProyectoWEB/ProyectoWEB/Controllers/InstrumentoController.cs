﻿using Microsoft.AspNetCore.Mvc;
using ProyectoWEB.Entities;
using ProyectoWEB.Models;

namespace ProyectoWEB.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class InstrumentoController : Controller
    {
        private readonly IInstrumentoModel _instrumentoModel;
        private readonly ICarritoModel _carritoModel;
        private IHostEnvironment _hostingEnvironment;
        public InstrumentoController(IInstrumentoModel instrumentoModel, ICarritoModel carritoModel, IHostEnvironment hostingEnvironment)
        {
            _instrumentoModel = instrumentoModel;
            _carritoModel = carritoModel;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarInstrumentos()
        {
            var datos = _instrumentoModel.ConsultarInstrumentos().Where(x => x.Cantidad > 0 && x.Estado == true).ToList();
            return View(datos);
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult MantenimientoInstrumentos()
        {
            var datos = _instrumentoModel.ConsultarInstrumentos();
            return View(datos);
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult RegistrarInstrumento()
        {
            ViewBag.Categorias = _instrumentoModel.ConsultarCategorias();
            return View();
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarPlatillos()
        {
            var datos = _instrumentoModel.ConsultarPlatillos().Where(x => x.Cantidad > 0 && x.Estado == true).ToList();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarBaterias()
        {
            var datos = _instrumentoModel.ConsultarBaterias().Where(x => x.Cantidad > 0 && x.Estado == true).ToList();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarBajos()
        {
            var datos = _instrumentoModel.ConsultarBajos().Where(x => x.Cantidad > 0 && x.Estado == true).ToList();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarGuitarras()
        {
            var datos = _instrumentoModel.ConsultarGuitarras().Where(x => x.Cantidad > 0 && x.Estado == true).ToList();
            return View(datos);
        }

        [HttpGet]
        //[FiltroSeguridad]
        public IActionResult ConsultarPianos()
        {
            var datos = _instrumentoModel.ConsultarPianos().Where(x => x.Cantidad > 0 && x.Estado == true).ToList();
            return View(datos);
        }

        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ActualizarInstrumento(long q)
        {
            var datos = _instrumentoModel.ConsultarInstrumentos().Where(x => x.IdInstrumento == q).FirstOrDefault();
            return View(datos);
        }



        [HttpGet]
        [FiltroSeguridad]
        public IActionResult ActualizarEstadoInstrumento(long q)
        {
            var entidad = new InstrumentoEnt();
            entidad.IdInstrumento = q;

            _instrumentoModel.ActualizarEstadoInstrumento(entidad);
            return RedirectToAction("MantenimientoInstrumentos", "Instrumento");
        }

        [HttpPost]
        [FiltroSeguridad]
        public IActionResult RegistrarInstrumento(IFormFile ImgInstrumento, InstrumentoEnt entidad)
        {
            string ext = Path.GetExtension(Path.GetFileName(ImgInstrumento.FileName));
            string folder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\imagenes");

            if (ext.ToLower() != ".png")
            {
                ViewBag.MensajePantalla = "La imagen debe ser .png";
                return View();
            }

            var IdInstrumento = _instrumentoModel.RegistrarInstrumento(entidad);

            if (IdInstrumento > 0)
            {
                string archivo = Path.Combine(folder, IdInstrumento + ext);
                using (Stream fileStream = new FileStream(archivo, FileMode.Create))
                {
                    ImgInstrumento.CopyTo(fileStream);
                }

                return RedirectToAction("ConsultarInstrumentos", "Instrumento");
            }

            ViewBag.MensajePantalla = "No se pudo registrar su producto";
            return View();

        }

        [HttpPost]
        [FiltroSeguridad]
        public IActionResult ActualizarInstrumento(IFormFile ImgInstrumento, InstrumentoEnt entidad)
        {
            string ext = string.Empty;
            string folder = string.Empty;
            string archivo = string.Empty;

            if (ImgInstrumento != null)
            {
                ext = Path.GetExtension(Path.GetFileName(ImgInstrumento.FileName));
                folder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\imagenes");
                archivo = Path.Combine(folder, entidad.IdInstrumento + ext);

                if (ext.ToLower() != ".png")
                {
                    ViewBag.MensajePantalla = "La imagen debe ser .png";
                    return View();
                }
            }

            var resp = _instrumentoModel.ActualizarInstrumento(entidad);

            var datos = _carritoModel.ConsultarCarrito();
            HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
            HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

            if (resp == 1)
            {
                if (ImgInstrumento != null)
                {
                    using (Stream fileStream = new FileStream(archivo, FileMode.Create))
                    {
                        ImgInstrumento.CopyTo(fileStream);
                    }
                }

                return RedirectToAction("MantenimientoInstrumentos", "Instrumento");
            }
            else
            {
                ViewBag.MensajePantalla = "No se pudo actualizar el producto";
                return View();
            }
        }
    } 
}
