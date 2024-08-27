using backend.Models;
using Microsoft.EntityFrameworkCore;

public class EstacionamentoContext : DbContext
{
    public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options) : base(options) { }

    public DbSet<Preco> Preco { get; set; }
    public DbSet<Veiculo> Veiculo { get; set; }
    public DbSet<Estacionamento> Estacionamento { get; set; }
}
