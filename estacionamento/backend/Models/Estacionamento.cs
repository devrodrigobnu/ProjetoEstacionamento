namespace backend.Models
{
    public class Estacionamento
    {
        public int Id { get; set; }

        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }

        public double ValorTotal { get; set; }

        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        public int PrecoId { get; set; }
        public Preco Preco { get; set; }
    }
}
