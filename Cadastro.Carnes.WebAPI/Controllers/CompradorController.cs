using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    // Define a rota base como "api/comprador" e marca como API controller
    [Route("api/[controller]")]
    [ApiController]
    public class CompradorController : ControllerBase
    {
        // Serviço responsável pela lógica de Comprador
        private readonly ICompradorService _service;

        // Construtor que injeta o serviço
        public CompradorController(ICompradorService service)
        {
            _service = service;
        }

        // Retorna todos os compradores cadastrados
        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _service.GetAll());

        // Busca um comprador específico pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comprador = await _service.GetById(id);
            return comprador == null ? NotFound() : Ok(comprador);
        }

        // Cria um novo comprador
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompradorDTO dto)
        {
            var resultado = await _service.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Atualiza os dados de um comprador existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompradorDTO dto)
        {
            var resultado = await _service.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        // Remove um comprador pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Retorna a quantidade total de compradores ativos
        [HttpGet("ativos")]
        public async Task<IActionResult> GetTotalCompradoresAtivos()
        {
            var total = await _service.GetTotalAtivos();
            return Ok(total);
        }
    }
}
