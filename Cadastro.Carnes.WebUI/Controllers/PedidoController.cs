using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
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
        var response = await _http.PostAsJsonAsync("api/pedido", dto);
        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _http.GetStringAsync($"api/pedido/{id}");
        return Content(result, "application/json");
    }

    [HttpPost]
    [Route("Pedido/Edit/{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] PedidoDTO dto)
    {
        var response = await _http.PutAsJsonAsync($"api/pedido/{id}", dto);
        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _http.DeleteAsync($"api/pedido/{id}");
        return Ok(await response.Content.ReadAsStringAsync());
    }
}
