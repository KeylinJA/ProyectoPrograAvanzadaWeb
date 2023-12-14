using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private string _connection;

        public UsuarioController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [Authorize]
        [Route("ConsultarUsuario")]
        public IActionResult ConsultarUsuario()
        {
            try
            {
                long IdUsuario = long.Parse(_utilitarios.Decrypt(User.Identity.Name.ToString()));

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<UsuarioEnt>("ConsultarUsuario",
                        new { IdUsuario },
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
        [Route("CambiarClave")]
        public IActionResult CambiarClave(UsuarioEnt entidad)
        {
            try
            {
                long IdUsuario = long.Parse(_utilitarios.Decrypt(User.Identity.Name.ToString()));

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("CambiarClave",
                        new { IdUsuario, entidad.contrasennaAnterior, entidad.Contrasenna },
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
        [Authorize]
        [Route("ConsultarUsuarios")]
        public IActionResult ConsultarUsuarios()
        {
            try
            {
                long IdUsuario = long.Parse(_utilitarios.Decrypt(User.Identity.Name.ToString()));

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<UsuarioEnt>("ConsultarUsuarios",
                        new { IdUsuario },
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
        [Route("ActualizarCuenta")]
        public IActionResult ActualizarCuenta(UsuarioEnt entidad)
        {
            try
            {
                long IdUsuario = long.Parse(_utilitarios.Decrypt(User.Identity.Name.ToString()));
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarCuenta",
                        new { entidad.Identificacion, entidad.Nombre, entidad.CorreoElectronico, IdUsuario },
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
