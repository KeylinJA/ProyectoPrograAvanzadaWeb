namespace ProyectoWEB.Entities
{
	public class UsuarioEnt
	{
		public long IdUsuario { get; set; }
		public string Identificacion { get; set; } = string.Empty;
		public string Nombre { get; set; } = string.Empty;
		public string CorreoElectronico { get; set; } = string.Empty;
		public string Contrasenna { get; set; } = string.Empty;
		public bool Estado { get; set; }
		public Byte IdRol { get; set; }
	}
}
