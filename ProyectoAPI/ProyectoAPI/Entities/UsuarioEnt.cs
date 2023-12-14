namespace ProyectoAPI.Entities
{
	public class UsuarioEnt
	{
        public long IdUsuario { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasenna { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public long IdRol { get; set; }

        public string contrasennaTemporal { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
        public string contrasennaAnterior { get; set; } = string.Empty;

    }
}
