using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    // Define a rota base como "api/moeda" e marca como API controller
    [Route("api/[controller]")]
    [ApiController]
    public class MoedaController : ControllerBase
    {
        // Serviço responsável pela lógica de Moeda
        private readonly IMoedaService _service;

        // Construtor que injeta o serviço de moeda
        public MoedaController(IMoedaService service)
        {
            _service = service;
        }

        // Retorna todas as moedas cadastradas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // Busca uma moeda específica pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        // Cria uma nova moeda
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MoedaDTO dto)
        {
            var resultado = await _service.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Atualiza os dados de uma moeda existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MoedaDTO dto)
        {
            dto.Id = id;
            var resultado = await _service.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        // Remove uma moeda pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
