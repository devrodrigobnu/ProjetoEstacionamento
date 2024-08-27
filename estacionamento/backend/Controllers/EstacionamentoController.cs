using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Repositories;
using backend.Services;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentosController : ControllerBase
    {
        private readonly EstacionamentoRepository _service;

        public EstacionamentosController(EstacionamentoRepository repository)
        {
            _service = repository;
        }

        [HttpGet("registros")]
        public async Task<ActionResult<List<Estacionamento>>> GetAll()
        {
            var result = await _service.GetAllVeiculosAsync();
            return Ok(result);
        }

        [HttpPost("entrada")]
        public async Task<IActionResult> RegistrarEntrada([FromBody] Estacionamento request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.RegistrarEntradaAsync(request.Veiculo.Placa, request.DataEntrada);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("saida")]
        public async Task<IActionResult> RegistrarSaida([FromBody] Estacionamento request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (request.DataSaida == null)
            {
                return BadRequest();
            }

            try
            {
                await _service.RegistrarSaidaAsync(request.Veiculo.Placa, request.DataSaida.Value);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletar/{placa}")]
        public async Task<IActionResult> RemoverRegistro(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return BadRequest();
            }

            var result = await _service.RemoverVeiculoPorPlacaAsync(placa);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarPlaca([FromQuery] string placaAntiga, [FromQuery] string placaNova)
        {
            if (string.IsNullOrWhiteSpace(placaAntiga) || string.IsNullOrWhiteSpace(placaNova))
            {
                return BadRequest();
            }

            var result = await _service.AtualizarPlacaVeiculosAsync(placaAntiga, placaNova);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("preco/{placa}")]
        public async Task<IActionResult> CalcularPreco(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return BadRequest();
            }

            try
            {
                var preco = await _service.CalcularPrecoVeiculoAsync(placa);
                return Ok(preco);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
