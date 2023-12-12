using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net.Http.Headers;
using ProyectoWEB.Entities;
using Microsoft.AspNetCore.Http;

namespace ProyectoWEB.Models
{
    public class InstrumentoModel : IInstrumentoModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public InstrumentoModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }
        public List<InstrumentoEnt>? ConsultarInstrumentos()
        {
            string url = _urlApi + "api/Instrumento/ConsultarInstrumentos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<InstrumentoEnt>>().Result;
            else
                return null;
        }

        public List<InstrumentoEnt>? ConsultarPlatillos()
        {
            string url = _urlApi + "api/Instrumento/ConsultarPlatillos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<InstrumentoEnt>>().Result;
            else
                return null;
        }

        public List<InstrumentoEnt>? ConsultarBaterias()
        {
            string url = _urlApi + "api/Instrumento/ConsultarBaterias";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<InstrumentoEnt>>().Result;
            else
                return null;
        }

        public List<InstrumentoEnt>? ConsultarBajos()
        {
            string url = _urlApi + "api/Instrumento/ConsultarBajos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<InstrumentoEnt>>().Result;
            else
                return null;
        }

        public List<InstrumentoEnt>? ConsultarGuitarras()
        {
            string url = _urlApi + "api/Instrumento/ConsultarGuitarras";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<InstrumentoEnt>>().Result;
            else
                return null;
        }

        public List<InstrumentoEnt>? ConsultarPianos()
        {
            string url = _urlApi + "api/Instrumento/ConsultarPianos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<InstrumentoEnt>>().Result;
            else
                return null;
        }

        public long RegistrarInstrumento(InstrumentoEnt entidad)
        {
            string url = _urlApi + "api/Instrumento/RegistrarInstrumento";
            //string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<long>().Result;
            else
                return 0;
        }

        public int ActualizarEstadoInstrumento(InstrumentoEnt entidad)
        {
            string url = _urlApi + "api/Instrumento/ActualizarEstadoInstrumento";
            //string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            JsonContent obj = JsonContent.Create(entidad);
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public List<SelectListItem>? ConsultarCategorias()
        {
            string url = _urlApi + "api/Instrumento/ConsultarCategorias";
            //string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            else
                return null;
        }

        public int ActualizarInstrumento(InstrumentoEnt entidad)
        {
            string url = _urlApi + "api/Instrumento/ActualizarInstrumento";
            //string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");

            JsonContent obj = JsonContent.Create(entidad);
           // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }


    }
}
