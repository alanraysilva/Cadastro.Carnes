using Cadastro.Carnes.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("API");

            var carnesResponse = await httpClient.GetStringAsync("api/carne/total");
            var compradoresResponse = await httpClient.GetStringAsync("api/comprador/ativos");
            var pedidosResponse = await httpClient.GetStringAsync("api/pedido/total");

            ViewBag.TotalCarnes = JsonSerializer.Deserialize<int>(carnesResponse);
            ViewBag.TotalCompradores = JsonSerializer.Deserialize<int>(compradoresResponse);
            ViewBag.TotalPedidos = JsonSerializer.Deserialize<int>(pedidosResponse);

            ViewData["Title"] = "Visão Geral";
            return View();
        }
    }
}