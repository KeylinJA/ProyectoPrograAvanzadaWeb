using System.Net.Mail;
using System.Text;

namespace ProyectoAPI.Entities
{
    public class Utilitarios : IUtilitarios
    {

        private readonly IConfiguration _configuration;
        private IHostEnvironment _hostingEnvironment;

        public Utilitarios(IConfiguration configuration, IHostEnvironment hostingEnvironment) 
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        public string GenerarCodigo()
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
        public string HTML(UsuarioEnt datos, string contrasennaTemporal)
        {
            string rutaArchivo = Path.Combine(_hostingEnvironment.ContentRootPath, "CorreosTemplate\\correo.html");
            string htmlArchivo = System.IO.File.ReadAllText(rutaArchivo);

            htmlArchivo = htmlArchivo.Replace("@@Nombre", datos.Nombre);
            htmlArchivo = htmlArchivo.Replace("@@ClaveTemporal", contrasennaTemporal);
            htmlArchivo = htmlArchivo.Replace("@@Link", "https://localhost:7244/Login/CambiarClaveCuenta?q=" + datos.IdUsuario.ToString());

            return htmlArchivo;
        }


        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
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
