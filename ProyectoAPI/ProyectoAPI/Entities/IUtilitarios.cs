using System.Security.Claims;

namespace ProyectoAPI.Entities
{
    public interface IUtilitarios
    {
        public string GenerarCodigo();
        public string HTML(UsuarioEnt datos, string contrasennaTemporal);
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);

        public string Encrypt(string texto);
        public long ObtenerUsuario(IEnumerable<Claim> valores);
        public string Decrypt(string texto);
        public string GenerarToken(string idUsuario);
    }
}
