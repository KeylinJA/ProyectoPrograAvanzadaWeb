namespace ProyectoAPI.Entities
{
    public interface IUtilitarios
    {
        public string GenerarCodigo();
        public string HTML(UsuarioEnt datos, string contrasennaTemporal);
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
    }
}
