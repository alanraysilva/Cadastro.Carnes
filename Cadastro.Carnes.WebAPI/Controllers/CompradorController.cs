using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Carnes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompradorController : ControllerBase
    {
        private readonly ICompradorService _service;

        public CompradorController(ICompradorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comprador = await _service.GetById(id);
            return comprador == null ? NotFound() : Ok(comprador);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompradorDTO dto)
        {
            var resultado = await _service.Add(dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompradorDTO dto)
        {
            var resultado = await _service.Update(dto);
            return resultado.Sucesso ? Ok(resultado) : NotFound(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.Remove(id);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
