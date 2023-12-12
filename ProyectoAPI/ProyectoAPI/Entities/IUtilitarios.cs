namespace ProyectoAPI.Entities
{
    public interface IUtilitarios
    {
        public string HTML(UsuarioEnt datos, string contrasennaTemporal);
        public string GenerarCodigo();
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
        public string Encrypt(string texto);
        public string Decrypt(string texto);
    }
}
