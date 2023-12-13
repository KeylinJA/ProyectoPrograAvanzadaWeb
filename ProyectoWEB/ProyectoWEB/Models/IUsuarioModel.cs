﻿using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
	public interface IUsuarioModel
	{
		public UsuarioEnt? IniciarSesion(UsuarioEnt entidad);
		public int RegistrarCuenta(UsuarioEnt entidad);
        public int ActualizarCuenta(UsuarioEnt entidad);
        public int RecuperarCuenta(UsuarioEnt entidad);
        public int CambiarClaveCuenta(UsuarioEnt entidad);
        public UsuarioEnt? ConsultarUsuario();
        public List<UsuarioEnt>? ConsultarUsuarios();

    }
}
