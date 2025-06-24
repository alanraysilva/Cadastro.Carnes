using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.Helpers;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class CarneController : Controller
    {
        // HttpClient para chamadas à API (injeção de dependência).
        private readonly HttpClient _http;

        // Construtor recebe o HttpClient já configurado para a API
        public CarneController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        // Exibe a tela principal com lista de carnes e origens
        public async Task<IActionResult> Index()
        {
            // Busca todas as carnes pela API
            var response = await _http.GetStringAsync("api/carne");
            var carnes = JsonSerializer.Deserialize<List<CarneDTO>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Busca todas as origens pela API (para preencher o select)
            CarneFormVm carne = new CarneFormVm
            {
                Carnes = carnes ?? new List<CarneDTO>(),
                Origem = JsonSerializer.Deserialize<List<OrigemDTO>>(await _http.GetStringAsync("api/origem"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<OrigemDTO>()
            };
            return View(carne);
        }

        // Cria uma nova carne (requisição AJAX do frontend)
        [HttpPost]
        [Route("Carne/Create")]
        public async Task<IActionResult> Create([FromBody] CarneDTO dto)
        {
            try
            {
                // Envia o DTO para a API via POST
                var response = await _http.PostAsJsonAsync("api/carne", dto);
                if (response.IsSuccessStatusCode)
                {
                    // Retorna sucesso para o JavaScript
                    return Json(new { success = true, message = "Carne cadastrada com sucesso!" });
                }
                else
                {
                    // Retorna erro detalhado da API
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao cadastrar carne: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                // Retorna erro inesperado (ex: API fora, timeout, etc)
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        // Atualiza uma carne existente
        [HttpPost]
        [Route("Carne/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CarneDTO dto)
        {
            try
            {
                // Envia o DTO para a API via PUT (edição)
                var response = await _http.PutAsJsonAsync($"api/carne/{id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Carne atualizada com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao atualizar carne: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        // Exclui uma carne pelo id
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Faz chamada DELETE na API
                var response = await _http.DeleteAsync($"api/carne/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Carne excluída com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao excluir carne: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }
    }
}
