using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class CidadeController : Controller
    {
        private readonly HttpClient _http;

        public CidadeController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        public async Task<IActionResult> Index()
        {
            var cidadesJson = await _http.GetStringAsync("api/cidade");
            var cidades = JsonSerializer.Deserialize<List<CidadeDTO>>(cidadesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            var vm = new CidadeFormVm { Cidades = cidades };
            return View(vm);
        }

        [HttpPost]
        [Route("Cidade/Create")]
        public async Task<IActionResult> Create([FromBody] CidadeDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/cidade", dto);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        [Route("Cidade/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CidadeDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"api/cidade/{id}", dto);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _http.DeleteAsync($"api/cidade/{id}");
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
