using Cadastro.Carnes.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // Factory para criar HttpClient configurado para API.
        private readonly IHttpClientFactory _httpClientFactory;

        // Injeta o factory no construtor
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Ação para a página inicial (dashboard)
        public async Task<IActionResult> Index()
        {
            // Cria o HttpClient com nome "API"
            var httpClient = _httpClientFactory.CreateClient("API");

            // Busca total de carnes cadastradas
            var carnesResponse = await httpClient.GetStringAsync("api/carne/total");
            // Busca total de compradores ativos
            var compradoresResponse = await httpClient.GetStringAsync("api/comprador/ativos");
            // Busca total de pedidos realizados
            var pedidosResponse = await httpClient.GetStringAsync("api/pedido/total");

            // Desserializa e envia os dados para a view via ViewBag
            ViewBag.TotalCarnes = JsonSerializer.Deserialize<int>(carnesResponse);
            ViewBag.TotalCompradores = JsonSerializer.Deserialize<int>(compradoresResponse);
            ViewBag.TotalPedidos = JsonSerializer.Deserialize<int>(pedidosResponse);

            // Define título da página
            ViewData["Title"] = "Visão Geral";
            return View();
        }
    }
}
