using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Repositories
{
    public class EstacionamentoRepository
    {
        private readonly EstacionamentoContext _context;

        public EstacionamentoRepository(EstacionamentoContext context)
        {
            _context = context;
        }

        public async Task<List<Estacionamento>> GetAllVeiculosAsync()
        {
            return await _context.Estacionamento
                .Include(e => e.Veiculo)
                .Include(e => e.Preco)
                .ToListAsync();
        }


        public async Task RegistrarEntradaAsync(string placa, DateTime dataEntrada)
        {
            if (string.IsNullOrEmpty(placa))
            {
                throw new ArgumentException(nameof(placa));
            }

            placa = placa.ToUpperInvariant();
            var entradaExistente = await _context.Estacionamento
                .FirstOrDefaultAsync(e => e.Veiculo.Placa == placa && e.DataSaida == null);

            if (entradaExistente != null)
            {
                throw new InvalidOperationException();
            }

            var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.Placa == placa);

            if (veiculo == null)
            {
                veiculo = new Veiculo
                {
                    Placa = placa
                };

                _context.Veiculo.Add(veiculo);
                await _context.SaveChangesAsync();
            }

            var preco = await _context.Preco.FirstOrDefaultAsync();

            if (preco == null)
            {
                throw new InvalidOperationException();
            }

            var entrada = new Estacionamento
            {
                VeiculoId = veiculo.Id,
                DataEntrada = dataEntrada,
                DataSaida = null,
                ValorTotal = 0,
                PrecoId = preco.Id
            };

            _context.Estacionamento.Add(entrada);
            await _context.SaveChangesAsync();
        }

        public async Task RegistrarSaidaAsync(string placa, DateTime? dataSaida)
        {
            if (string.IsNullOrEmpty(placa))
            {
                throw new ArgumentException(nameof(placa));
            }

            if (!dataSaida.HasValue)
            {
                throw new ArgumentException(nameof(dataSaida));
            }

            placa = placa.ToUpperInvariant();

            var entrada = await _context.Estacionamento
                .FirstOrDefaultAsync(e => e.Veiculo.Placa == placa && e.DataSaida == null);

            if (entrada == null)
            {
                throw new ArgumentException();
            }

            if (entrada.DataSaida.HasValue)
            {
                throw new InvalidOperationException();
            }

            entrada.DataSaida = dataSaida.Value;

            _context.Estacionamento.Update(entrada);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> RemoverVeiculoPorPlacaAsync(string placa)
        {
            placa = placa.ToUpperInvariant();
            var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.Placa == placa);

            if (veiculo == null)
            {
                return false;
            }

            _context.Veiculo.Remove(veiculo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarPlacaVeiculosAsync(string placaAntiga, string placaNova)
        {
            placaAntiga = placaAntiga.ToUpperInvariant();
            placaNova = placaNova.ToUpperInvariant();
            var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.Placa == placaAntiga);

            if (veiculo == null)
            {
                return false;
            }

            veiculo.Placa = placaNova;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<double> CalcularPrecoVeiculoAsync(string placa)
        {
            placa = placa.ToUpperInvariant();
            var registro = await _context.Estacionamento
                .Include(e => e.Veiculo)
                .FirstOrDefaultAsync(e => e.Veiculo.Placa == placa && e.DataSaida != null);

            if (registro == null)
            {
                throw new ArgumentException();
            }

            var preco = await _context.Preco.FirstOrDefaultAsync();
            if (preco == null)
            {
                throw new InvalidOperationException();
            }
            var valorHora = preco.ValorHora;

            var duracaoMinutos = (registro.DataSaida.Value - registro.DataEntrada).TotalMinutes;
            var horasCompletas = Math.Ceiling(duracaoMinutos / 60);
            var valorTotal = horasCompletas * valorHora;

            if (duracaoMinutos <= 60)
            {
                valorTotal = Math.Max(valorTotal, valorHora);
            }

            registro.ValorTotal = valorTotal;

            _context.Estacionamento.Update(registro);
            await _context.SaveChangesAsync();

            return registro.ValorTotal;
        }

    }
}
