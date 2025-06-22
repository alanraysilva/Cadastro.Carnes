using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.Helpers;
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
            try
            {
                var response = await _http.PostAsJsonAsync("api/cidade", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Cidade cadastrada com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao cadastrar cidade: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        [HttpPost]
        [Route("Cidade/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CidadeDTO dto)
        {
            try
            {
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

        [HttpPost]
        [Route("Cidade/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
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
