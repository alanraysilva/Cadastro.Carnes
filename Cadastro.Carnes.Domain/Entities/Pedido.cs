using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um pedido realizado pelo comprador, incluindo a lista de itens e valor total.
    /// </summary>
    public class Pedido
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Data em que o pedido foi realizado.
        /// </summary>
        public DateTime Data { get; private set; }

        /// <summary>
        /// Identificador do comprador relacionado a este pedido.
        /// </summary>
        public int CompradorId { get; private set; }

        /// <summary>
        /// Valor total do pedido, calculado com base nos itens.
        /// </summary>
        public decimal Total { get; private set; }

        /// <summary>
        /// Lista dos itens que compõem o pedido.
        /// </summary>
        public List<ItemPedido> Itens { get; private set; } = new();

        /// <summary>
        /// Propriedade de navegação para o comprador (opcional).
        /// </summary>
        public Comprador? Comprador { get; set; }

        /// <summary>
        /// Construtor vazio necessário para o Entity Framework.
        /// </summary>
        public Pedido() { }

        /// <summary>
        /// Construtor que permite criar um pedido já com o Id definido e lista de itens.
        /// </summary>
        public Pedido(int id, DateTime data, int compradorId, List<ItemPedido> itens)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(data, compradorId);
            AtualizarItens(itens);
        }

        /// <summary>
        /// Construtor que cria um novo pedido, sem Id definido (usado em operações de cadastro).
        /// </summary>
        public Pedido(DateTime data, int compradorId, List<ItemPedido> itens)
        {
            ValidateDomain(data, compradorId);
            AtualizarItens(itens);
        }

        /// <summary>
        /// Atualiza os dados do pedido, incluindo data, comprador e itens.
        /// </summary>
        public void Update(DateTime data, int compradorId, List<ItemPedido> itens)
        {
            ValidateDomain(data, compradorId);
            AtualizarItens(itens);
        }

        /// <summary>
        /// Valida os campos obrigatórios do pedido.
        /// </summary>
        private void ValidateDomain(DateTime data, int compradorId)
        {
            DomainExceptionValidation.When(compradorId <= 0, "Comprador inválido");
            Data = data;
            CompradorId = compradorId;
        }

        /// <summary>
        /// Atualiza a lista de itens do pedido e recalcula o total.
        /// </summary>
        private void AtualizarItens(List<ItemPedido> itens)
        {
            DomainExceptionValidation.When(itens == null || !itens.Any(), "Pedido deve conter ao menos um item");

            Itens = itens!;
            Total = Itens!.Sum(i => i.Quantidade * i.Valor);
        }
    }
}
