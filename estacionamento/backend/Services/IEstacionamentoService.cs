using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public interface IEstacionamentoService
    {
        Task<List<Estacionamento>> GetAllVeiculosAsync();
        Task<IActionResult> RegistrarEntradaAsync(string placa, DateTime dataEntrada);
        Task<IActionResult> RegistrarSaidaAsync(string placa, DateTime dataSaida);
        Task<bool> RemoverVeiculoPorPlacaAsync(string placa);
        Task<bool> AtualizarPlacaVeiculosAsync(string placaAntiga, string placaNova);
        Task<double> CalcularPrecoVeiculoAsync(string placa);
    }
}
