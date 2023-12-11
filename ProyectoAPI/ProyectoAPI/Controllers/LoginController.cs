using Dapper;
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
        private IHostEnvironment _hostingEnvironment;
        private string _connection;

        public LoginController(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _hostingEnvironment = hostingEnvironment;
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

                    string contrasennaTemporal = GenerarCodigo();
                    string contenido = HTML(datos, contrasennaTemporal);

                     context.Execute("ActualizarClaveTemp",
                        new { datos.IdUsuario, contrasennaTemporal },
                        commandType: CommandType.StoredProcedure);

                    EnviarCorreo(datos.CorreoElectronico, "Recuperar Contraseña", contenido);

                    return Ok(1);
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

        private string GenerarCodigo()
        {
            int length = 4;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        //FALTA ENCRIPTAR
        private string HTML(UsuarioEnt datos, string contrasennaTemporal)
        {
            string rutaArchivo = Path.Combine(_hostingEnvironment.ContentRootPath, "CorreosTemplate\\correo.html");
            string htmlArchivo = System.IO.File.ReadAllText(rutaArchivo);

            htmlArchivo = htmlArchivo.Replace("@@Nombre", datos.Nombre);
            htmlArchivo = htmlArchivo.Replace("@@ClaveTemporal", contrasennaTemporal);
            htmlArchivo = htmlArchivo.Replace("@@Link", "https://localhost:7244/Login/CambiarClaveCuenta?q=" + datos.IdUsuario.ToString());

            return htmlArchivo;
        }


        private void EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
        {
            string correoGmail = _configuration.GetSection("Llaves:correoGmail").Value;
            string tokenAplicacionGmail = _configuration.GetSection("Llaves:tokenAplicacionGmail").Value;

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(Destinatario));
            msg.From = new MailAddress(correoGmail);
            msg.Subject = Asunto;
            msg.Body = Mensaje;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(correoGmail, tokenAplicacionGmail);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Send(msg);
        }



    }
}
