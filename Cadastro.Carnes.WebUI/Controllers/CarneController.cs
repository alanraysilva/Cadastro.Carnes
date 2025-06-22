using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.Helpers;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class CarneController : Controller
    {
        private readonly HttpClient _http;

        public CarneController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _http.GetStringAsync("api/carne");
            var carnes = JsonSerializer.Deserialize<List<CarneDTO>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            CarneFormVm carne = new CarneFormVm             {
                Carnes = carnes ?? new List<CarneDTO>(),
                Origem = JsonSerializer.Deserialize<List<OrigemDTO>>(await _http.GetStringAsync("api/origem"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<OrigemDTO>()
            };
            return View(carne);
        }

        [HttpPost]
        [Route("Carne/Create")]
        public async Task<IActionResult> Create([FromBody] CarneDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/carne", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Carne cadastrada com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao cadastrar carne: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        [HttpPost]
        [Route("Carne/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CarneDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/carne/{id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Carne atualizada com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao atualizar carne: " + JsonHelper.GetMessage(errorMsg)});
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
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
