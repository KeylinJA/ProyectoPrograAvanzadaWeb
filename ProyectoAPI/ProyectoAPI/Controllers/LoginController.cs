﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private string _connection;

        public LoginController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<UsuarioEnt>("IniciarSesion",
                        new { entidad.CorreoElectronico, entidad.Contrasenna },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (datos != null)
                    {
                        return Ok(datos);
                    }
                    else
                    {
                        // No user found, return 404 status code
                        return NotFound("User not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("RegistrarCuenta")]
        public IActionResult RegistrarCuenta(UsuarioEnt entidad)
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    entidad.Estado = true;
                    var datos = context.Execute("RegistrarCuenta",
                        new { entidad.Identificacion, entidad.Nombre, entidad.CorreoElectronico, entidad.Contrasenna, entidad.Estado },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RecuperarCuenta")]
        public IActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<UsuarioEnt>("RecuperarCuenta",
                        new { entidad.CorreoElectronico },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (datos != null)
                    {

                        string contrasennaTemporal = _utilitarios.GenerarCodigo();
                        string contenido = _utilitarios.HTML(datos, contrasennaTemporal);

                        context.Execute("ActualizarClaveTemp",
                           new { datos.IdUsuario, contrasennaTemporal },
                           commandType: CommandType.StoredProcedure);

                        _utilitarios.EnviarCorreo(datos.CorreoElectronico, "Recuperar Contraseña", contenido);

                        return Ok(1);
                    }
                    else
                        return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("CambiarClaveCuenta")]
        public IActionResult CambiarClaveCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("CambiarClaveCuenta",
                        new { entidad.IdUsuario, entidad.contrasennaTemporal, entidad.Contrasenna },
                        commandType: CommandType.StoredProcedure);

                    return Ok(1);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
