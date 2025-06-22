using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class CompradorController : Controller
    {
        private readonly HttpClient _http;
        public CompradorController(IHttpClientFactory factory) => _http = factory.CreateClient("API");

        public async Task<IActionResult> Index()
        {
            var compradores = JsonSerializer.Deserialize<List<CompradorDTO>>(
                await _http.GetStringAsync("api/comprador"),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CompradorDTO>();

            var cidades = JsonSerializer.Deserialize<List<CidadeDTO>>(
                await _http.GetStringAsync("api/cidade"),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CidadeDTO>();

            var vm = new CompradorFormVm { Compradores = compradores, Cidades = cidades };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompradorDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/comprador", dto);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] CompradorDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"api/comprador/{id}", dto);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _http.DeleteAsync($"api/comprador/{id}");
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
