using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.Helpers;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class CidadeController : Controller
    {
        // HttpClient usado para fazer chamadas à API
        private readonly HttpClient _http;

        // O HttpClient é injetado no construtor, já configurado com o endpoint da API
        public CidadeController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        // Exibe a tela principal com a listagem de cidades
        public async Task<IActionResult> Index()
        {
            // Busca a lista de cidades na API e faz o parse para a viewmodel
            var cidadesJson = await _http.GetStringAsync("api/cidade");
            var cidades = JsonSerializer.Deserialize<List<CidadeDTO>>(cidadesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            var vm = new CidadeFormVm { Cidades = cidades };
            return View(vm);
        }

        // Cria uma nova cidade via requisição AJAX (frontend)
        [HttpPost]
        [Route("Cidade/Create")]
        public async Task<IActionResult> Create([FromBody] CidadeDTO dto)
        {
            try
            {
                // Envia o DTO para a API via POST
                var response = await _http.PostAsJsonAsync("api/cidade", dto);
                if (response.IsSuccessStatusCode)
                {
                    // Resposta de sucesso para o frontend
                    return Json(new { success = true, message = "Cidade cadastrada com sucesso!" });
                }
                else
                {
                    // Resposta de erro retornada pela API
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao cadastrar cidade: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                // Resposta de erro inesperado (ex: timeout, API fora do ar)
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        // Atualiza uma cidade existente
        [HttpPost]
        [Route("Cidade/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CidadeDTO dto)
        {
            try
            {
                // Envia o DTO para a API via PUT (edição)
                var response = await _http.PutAsJsonAsync($"api/cidade/{id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Cidade atualizada com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao atualizar cidade: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        // Remove uma cidade pelo id
        [HttpPost]
        [Route("Cidade/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Chama o endpoint DELETE da API
                var response = await _http.DeleteAsync($"api/cidade/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Cidade excluída com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao excluir cidade: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }
    }
}
