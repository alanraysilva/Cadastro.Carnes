using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    // Define a rota base do controller como "api/carne" e indica que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class CarneController : ControllerBase
    {
        // Injeção do serviço responsável pela lógica de negócios da entidade Carne
        private readonly ICarneService _carneService;

        // Construtor recebe o serviço pelo mecanismo de injeção de dependência
        public CarneController(ICarneService carneService)
        {
            _carneService = carneService;
        }

        // Retorna a lista de todas as carnes cadastradas
        [HttpGet]
        public async Task<IActionResult> Get() =>
           Ok(await _carneService.GetAll());

        // Retorna uma carne específica pelo seu ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var carne = await _carneService.GetById(id);
            return carne == null ? NotFound() : Ok(carne);
        }

        // Cadastra uma nova carne
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarneDTO dto)
        {
            var resultado = await _carneService.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Atualiza uma carne existente pelo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarneDTO dto)
        {
            var resultado = await _carneService.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        // Remove uma carne pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var resultado = await _carneService.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        // Retorna o total de carnes cadastradas
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalCarnes()
        {
            var total = await _carneService.GetTotalCount();
            return Ok(total);
        }
    }
}
