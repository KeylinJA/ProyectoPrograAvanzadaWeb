using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
	public interface IUsuarioModel
	{
		public UsuarioEnt? IniciarSesion(UsuarioEnt entidad);
		public int RegistrarCuenta(UsuarioEnt entidad);
		public int RecuperarCuenta(UsuarioEnt entidad);

	}
}
