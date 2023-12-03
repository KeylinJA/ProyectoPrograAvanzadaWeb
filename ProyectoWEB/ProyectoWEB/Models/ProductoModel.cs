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
    }
}
