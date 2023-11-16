﻿using ProyectoWEB.Entities;
using System.Configuration;

namespace ProyectoWEB.Models
{
    public class UsuarioModel : IUsuarioModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _urlApi;

        public UsuarioModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }

        public UsuarioEnt? IniciarSesion(UsuarioEnt entidad)
        {
            string url = _urlApi + "api/Login/IniciarSesion";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
            else
                return null;
        }
        public int RegistrarCuenta(UsuarioEnt entidad)
        {
            string url = _urlApi + "api/Login/RegistrarCuenta";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
        public int RecuperarCuenta(UsuarioEnt entidad)
        {
            string url = _urlApi + "api/Login/RecuperarCuenta";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;

        }
    }
}
