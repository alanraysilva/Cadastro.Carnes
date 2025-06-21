using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarneController : ControllerBase
    {
        private readonly ICarneService _carneService;

        public CarneController(ICarneService carneService)
        {
            _carneService = carneService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
           Ok(await _carneService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var carne = await _carneService.GetById(id);
            return carne == null ? NotFound() : Ok(carne);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarneDTO dto)
        {
            var resultado = await _carneService.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarneDTO dto)
        {
            var resultado = await _carneService.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var resultado = await _carneService.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
