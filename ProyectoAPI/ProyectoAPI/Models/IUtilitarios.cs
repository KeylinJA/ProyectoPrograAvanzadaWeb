using System.Security.Claims;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Models
{
    public interface IUtilitarios
    {
        public string GenerarCodigo();
        public string HTML(UsuarioEnt datos, string contrasennaTemporal);
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);

        public string Encrypt(string texto);
        public string Decrypt(string texto);
        public string GenerarToken(string IdUsuario, string IdRol);

        public long ObtenerUsuario(IEnumerable<Claim> valores);

        public bool IsAdmin(IEnumerable<Claim> valores);
    }
}
