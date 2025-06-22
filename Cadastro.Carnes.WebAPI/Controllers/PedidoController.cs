using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    // Controlador para gerenciamento dos pedidos via API REST
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        // Serviço responsável pelas operações de pedido
        private readonly IPedidoService _pedidoService;

        // Injeta o serviço no construtor
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // Retorna a lista de todos os pedidos cadastrados
        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _pedidoService.GetAll());

        // Busca um pedido específico pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pedido = await _pedidoService.GetById(id);
            return pedido == null ? NotFound() : Ok(pedido);
        }

        // Cria um novo pedido
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidoDTO dto)
        {
            var resultado = await _pedidoService.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Atualiza um pedido existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PedidoDTO dto)
        {
            var resultado = await _pedidoService.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        // Remove um pedido pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _pedidoService.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Retorna a quantidade total de pedidos cadastrados
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalPedidos()
        {
            var total = await _pedidoService.GetTotalCount();
            return Ok(total);
        }
    }
}
