using System.Security.Claims;

namespace ProyectoAPI.Models
{
    public interface IUtilitarios
    {
        public string Encrypt(string texto);

        public string Decrypt(string texto);

        public long ObtenerUsuario(IEnumerable<Claim> valores);
    }
}
