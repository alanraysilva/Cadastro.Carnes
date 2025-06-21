using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        public int CompradorId { get; private set; }
        public decimal Total { get; private set; }
        public List<ItemPedido> Itens { get; private set; } = new();

        public Comprador? Comprador { get; set; }

        public Pedido() { }

        public Pedido(int id, DateTime data, int compradorId, List<ItemPedido> itens)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(data, compradorId);
            AtualizarItens(itens);
        }

        public Pedido(DateTime data, int compradorId, List<ItemPedido> itens)
        {
            ValidateDomain(data, compradorId);
            AtualizarItens(itens);
        }

        public void Update(DateTime data, int compradorId, List<ItemPedido> itens)
        {
            ValidateDomain(data, compradorId);
            AtualizarItens(itens);
        }

        private void ValidateDomain(DateTime data, int compradorId)
        {
            DomainExceptionValidation.When(compradorId <= 0, "Comprador inválido");
            Data = data;
            CompradorId = compradorId;
        }

        private void AtualizarItens(List<ItemPedido> itens)
        {
            DomainExceptionValidation.When(itens == null || !itens.Any(), "Pedido deve conter ao menos um item");

            Itens = itens!;
            Total = Itens!.Sum(i => i.Quantidade * i.Valor);
        }
    }
}
