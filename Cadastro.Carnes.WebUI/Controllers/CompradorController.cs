using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.Helpers;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class CompradorController : Controller
    {
        // HttpClient configurado para chamar a API
        private readonly HttpClient _http;

        // Injeta HttpClient no construtor
        public CompradorController(IHttpClientFactory factory) => _http = factory.CreateClient("API");

        // Tela principal que lista compradores e cidades para combo
        public async Task<IActionResult> Index()
        {
            // Busca lista de compradores da API e desserializa
            var compradores = JsonSerializer.Deserialize<List<CompradorDTO>>(
                await _http.GetStringAsync("api/comprador"),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CompradorDTO>();

            // Busca lista de cidades da API e desserializa
            var cidades = JsonSerializer.Deserialize<List<CidadeDTO>>(
                await _http.GetStringAsync("api/cidade"),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CidadeDTO>();

            // Prepara a ViewModel com dados para a view
            var vm = new CompradorFormVm { Compradores = compradores, Cidades = cidades };
            return View(vm);
        }

        // Cria um novo comprador via POST AJAX
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompradorDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/comprador", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Comprador cadastrado com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao cadastrar comprador: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        // Atualiza um comprador existente via POST AJAX
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] CompradorDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/comprador/{id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Comprador atualizado com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao atualizar comprador: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }

        // Exclui um comprador via POST AJAX
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/comprador/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Comprador excluído com sucesso!" });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Erro ao excluir comprador: " + JsonHelper.GetMessage(errorMsg) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
            }
        }
    }
}
