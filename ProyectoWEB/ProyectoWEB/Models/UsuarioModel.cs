using ProyectoWEB.Entities;
using System.Configuration;
using System.Net.Http.Headers;

namespace ProyectoWEB.Models
{
    public class UsuarioModel : IUsuarioModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public UsuarioModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
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


        public int CambiarClaveCuenta(UsuarioEnt entidad)
        {
            string url = _urlApi + "api/Login/CambiarClaveCuenta";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public UsuarioEnt? ConsultarUsuario()
        {
            string url = _urlApi + "api/Usuario/ConsultarUsuario";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
            else
                return null;
        }

        public List<UsuarioEnt>? ConsultarUsuarios()
        {
            string url = _urlApi + "api/Usuario/ConsultarUsuarios";
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<UsuarioEnt>>().Result;
            else
                return null;
        }

    }
}
