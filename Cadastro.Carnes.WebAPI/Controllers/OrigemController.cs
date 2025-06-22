using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    // Define a rota base como "api/origem" e marca como controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class OrigemController : ControllerBase
    {
        // Serviço responsável pelas regras de negócio da entidade Origem
        private readonly IOrigemService _service;

        // Injeta o serviço pelo construtor
        public OrigemController(IOrigemService service)
        {
            _service = service;
        }

        // Retorna todas as origens cadastradas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // Busca uma origem específica pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        // Cria uma nova origem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrigemDTO dto)
        {
            var resultado = await _service.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Atualiza uma origem existente pelo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrigemDTO dto)
        {
            dto.Id = id; // Garante que o DTO tenha o ID correto
            var resultado = await _service.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        // Remove uma origem pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
