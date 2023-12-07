using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
    public class ProductoModel : IProductoModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public ProductoModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            //_HttpContextAccessor = httpContextAccessor;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }

        public List<ProductoEnt>? ConsultarProductos()
        {
            string url = _urlApi + "api/Producto/ConsultarProductos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Carrito()
        {
            string url = _urlApi + "api/Producto/Carrito";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Detalle()
        {
            string url = _urlApi + "api/Producto/Detalle";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Platillos()
        {
            string url = _urlApi + "api/Producto/Platillos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Baterias()
        {
            string url = _urlApi + "api/Producto/Baterias";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Bajos()
        {
            string url = _urlApi + "api/Producto/Bajos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Guitarras()
        {
            string url = _urlApi + "api/Producto/Guitarras";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? AudioVideo()
        {
            string url = _urlApi + "api/Producto/AudioVideo";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Viento()
        {
            string url = _urlApi + "api/Producto/Viento";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? PianosTeclados()
        {
            string url = _urlApi + "api/Producto/PianosTeclados";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }

        public List<ProductoEnt>? Bases()
        {
            string url = _urlApi + "api/Producto/Bases";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            else
                return null;
        }
    }
}
