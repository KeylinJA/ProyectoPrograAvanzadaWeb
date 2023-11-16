using ProyectoWEB.Entities;
using System.Configuration;
using System.Net.Http.Headers;

namespace ProyectoWEB.Models
{
    public class ProductoModel : IProductoModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public ProductoModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }

        public List<ProductosEnt>? ConsultarProductos()
        {
            string url = _urlApi + "api/Usuario/ConsultarProductos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<ProductosEnt>>().Result;
            else
                return null;
        }
    }
}
