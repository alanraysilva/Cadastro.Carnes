using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class ItemPedido
    {
        public int Id { get; private set; }
        public int PedidoId { get; private set; }
        public int CarneId { get; private set; }
        public int Quantidade { get; private set; }
        public int MoedaId { get; private set; }
        public decimal Valor { get; private set; }

        public Carne? Carne { get; set; }
        public Moeda? Moeda { get; set; }
        public Pedido? Pedido { get; set; }
        

        public ItemPedido() { }

        public ItemPedido(int id, int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(pedidoId, carneId, quantidade, moedaId, valor);
        }

        public ItemPedido(int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            ValidateDomain(pedidoId, carneId, quantidade, moedaId, valor);
        }

        public void Update(int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            ValidateDomain(pedidoId, carneId, quantidade, moedaId, valor);
        }

        private void ValidateDomain(int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            DomainExceptionValidation.When(pedidoId < 0, "Pedido inválido");
            DomainExceptionValidation.When(carneId < 0, "Carne inválida");
            DomainExceptionValidation.When(quantidade <= 0, "Quantidade não pode ser menor ou igual a zero");
            DomainExceptionValidation.When(moedaId < 0, "Moeda inválida");
            DomainExceptionValidation.When(valor <= 0, "O valor não pode ser menor ou igual a zero");

            PedidoId = pedidoId;
            CarneId = carneId;
            Quantidade = quantidade;
            MoedaId = moedaId;
            Valor = valor;

        }
    }
}
