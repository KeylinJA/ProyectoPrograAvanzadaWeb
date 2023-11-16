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
        private string _connection;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
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

                    return Ok(datos);

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

                    EnviarCorreo(datos.CorreoElectronico, "Recuperar Contraseña", HTML(datos.Nombre));

                    return Ok(1);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string HTML(string Nombre)
        {
            string rutaArchivo = @"C:\correo.html";
            string htmlArchivo = System.IO.File.ReadAllText(rutaArchivo);
            return htmlArchivo.Replace("@@Nombre", Nombre);
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
