﻿using ProyectoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;

        public InstrumentoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        [Route("ConsultarInstrumentos")]
        public IActionResult ConsultarInstrumentos()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<InstrumentoEnt>("ConsultarInstrumentos",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarPlatillos")]
        public IActionResult Platillos()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<InstrumentoEnt>("ConsultarPlatillos",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarBaterias")]
        public IActionResult Baterias()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<InstrumentoEnt>("ConsultarBaterias",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarBajos")]
        public IActionResult Bajos()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<InstrumentoEnt>("ConsultarBajos",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarGuitarras")]
        public IActionResult Guitarras()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<InstrumentoEnt>("ConsultarGuitarras",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("ConsultarPianos")]
        public IActionResult PianosTeclados()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<InstrumentoEnt>("ConsultarPianos",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegistrarInstrumento")]
        [Authorize]
        public IActionResult RegistrarInstrumento(InstrumentoEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<long>("RegistrarInstrumento",
                        new { entidad.Nombre, entidad.Descripcion, entidad.IdCategoria, entidad.Cantidad, entidad.Precio },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Authorize]
        [Route("ActualizarEstadoInstrumento")]
        public IActionResult ActualizarEstadoInstrumento(InstrumentoEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarEstadoInstrumento",
                        new { entidad.IdInstrumento },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarCategorias")]
        public IActionResult ConsultarCategorias()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<SelectListItem>("ConsultarCategorias",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("ActualizarInstrumento")]
        public IActionResult ActualizarInstrumento(InstrumentoEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarInstrumento",
                        new { entidad.Nombre, entidad.Descripcion, entidad.Precio, entidad.Cantidad, entidad.IdInstrumento },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
