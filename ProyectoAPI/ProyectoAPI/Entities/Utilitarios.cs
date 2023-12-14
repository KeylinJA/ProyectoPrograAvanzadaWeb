using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;

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

        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("EzfjS0IHnNSjv0jo");
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public long ObtenerUsuario(IEnumerable<Claim> valores)
        {
            var claims = valores.Select(Claim => new { Claim.Type, Claim.Value }).ToArray();
            return long.Parse(Decrypt(claims.Where(x => x.Type == "username").ToList().FirstOrDefault().Value));
        }


        public string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("EzfjS0IHnNSjv0jo");
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string GenerarToken(string IdUsuario, string IdRol)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("username", Encrypt(IdUsuario)));
            claims.Add(new Claim("userrol", Encrypt(IdRol)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ty1UELmVFKQmMD4af0a4jvfZS30cXu3U"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public void ObtenerClaims(IEnumerable<Claim> valores, ref string username, ref string userrol, ref bool isAdmin)
        {
            var claims = valores.Select(Claim => new { Claim.Type, Claim.Value }).ToArray();
            username = Decrypt(claims.Where(x => x.Type == "username").ToList().FirstOrDefault().Value);
            userrol = Decrypt(claims.Where(x => x.Type == "userrol").ToList().FirstOrDefault().Value);

            if (userrol == "1")
                isAdmin = true;
        }
    }
}
