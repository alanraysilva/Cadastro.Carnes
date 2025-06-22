using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    // Define a rota base "api/cidade" e marca como controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        // Serviço responsável pelas regras de negócio da entidade Cidade
        private readonly ICidadeService _service;

        // Injeção do serviço no construtor
        public CidadeController(ICidadeService service)
        {
            _service = service;
        }

        // Retorna todas as cidades cadastradas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // Busca uma cidade específica pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        // Cadastra uma nova cidade
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CidadeDTO dto)
        {
            var resultado = await _service.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Atualiza os dados de uma cidade pelo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CidadeDTO dto)
        {
            dto.Id = id; // Garante que o ID será atualizado corretamente
            var resultado = await _service.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        // Remove uma cidade pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
