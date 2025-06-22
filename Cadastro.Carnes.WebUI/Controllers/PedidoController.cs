using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.Helpers;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

public class PedidoController : Controller
{
    private readonly HttpClient _http;

    public PedidoController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
    }

    public async Task<IActionResult> Index()
    {
        var pedidos = JsonSerializer.Deserialize<List<PedidoDTO>>(await _http.GetStringAsync("api/pedido"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<PedidoDTO>();
        var compradores = JsonSerializer.Deserialize<List<CompradorDTO>>(await _http.GetStringAsync("api/comprador"),new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CompradorDTO>();
        var carnes = JsonSerializer.Deserialize<List<CarneDTO>>(await _http.GetStringAsync("api/carne"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CarneDTO>();
        var moedas = JsonSerializer.Deserialize<List<MoedaDTO>>(await _http.GetStringAsync("api/moeda"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<MoedaDTO>();

        PedidoFormVm model = new PedidoFormVm
        {
            Pedidos = pedidos,
            Compradores = compradores,
            Carnes = carnes,
            Moedas = moedas
        };
        return View(model);
    }

    [HttpPost]
    [Route("Pedido/Create")]
    public async Task<IActionResult> Create([FromBody] PedidoDTO dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/pedido", dto);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Pedido cadastrado com sucesso!" });
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = "Erro ao cadastrar pedido: " + JsonHelper.GetMessage(errorMsg) });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
        }
    }

    [HttpPost]
    [Route("Pedido/Edit/{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] PedidoDTO dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/pedido/{id}", dto);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Pedido atualizado com sucesso!" });
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = "Erro ao atualizar pedido: " + JsonHelper.GetMessage(errorMsg) });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
        }
    }

    [HttpPost]
    [Route("Pedido/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/pedido/{id}");
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Pedido excluído com sucesso!" });
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = "Erro ao excluir pedido: " + JsonHelper.GetMessage(errorMsg) });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Erro inesperado: " + JsonHelper.GetMessage(ex.Message) });
        }
    }
}
