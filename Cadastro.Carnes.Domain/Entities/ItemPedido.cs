using Cadastro.Carnes.Domain.Validation;

namespace Cadastro.Carnes.Domain.Entities
{
    /// <summary>
    /// Representa um item vinculado a um pedido, com informações sobre a carne, quantidade, moeda e valor.
    /// </summary>
    public class ItemPedido
    {
        /// <summary>
        /// Identificador único do item do pedido.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Identificador do pedido ao qual o item está vinculado.
        /// </summary>
        public int PedidoId { get; private set; }

        /// <summary>
        /// Identificador da carne referente a este item.
        /// </summary>
        public int CarneId { get; private set; }

        /// <summary>
        /// Quantidade de carnes deste item no pedido.
        /// </summary>
        public int Quantidade { get; private set; }

        /// <summary>
        /// Identificador da moeda utilizada neste item.
        /// </summary>
        public int MoedaId { get; private set; }

        /// <summary>
        /// Valor unitário da carne neste item, na moeda escolhida.
        /// </summary>
        public decimal Valor { get; private set; }

        /// <summary>
        /// Propriedade de navegação para a entidade Carne.
        /// </summary>
        public Carne? Carne { get; set; }

        /// <summary>
        /// Propriedade de navegação para a entidade Moeda.
        /// </summary>
        public Moeda? Moeda { get; set; }

        /// <summary>
        /// Propriedade de navegação para a entidade Pedido.
        /// </summary>
        public Pedido? Pedido { get; set; }

        /// <summary>
        /// Construtor vazio necessário para o Entity Framework.
        /// </summary>
        public ItemPedido() { }

        /// <summary>
        /// Construtor para instanciar o item com todos os campos, incluindo o Id.
        /// </summary>
        public ItemPedido(int id, int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            DomainExceptionValidation.When(id < 0, "Código inválido");
            Id = id;
            ValidateDomain(pedidoId, carneId, quantidade, moedaId, valor);
        }

        /// <summary>
        /// Construtor para instanciar o item sem o Id, geralmente para novos itens.
        /// </summary>
        public ItemPedido(int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            ValidateDomain(pedidoId, carneId, quantidade, moedaId, valor);
        }

        /// <summary>
        /// Atualiza os campos do item do pedido, aplicando as regras de validação.
        /// </summary>
        public void Update(int pedidoId, int carneId, int quantidade, int moedaId, decimal valor)
        {
            ValidateDomain(pedidoId, carneId, quantidade, moedaId, valor);
        }

        /// <summary>
        /// Valida os dados obrigatórios do item do pedido, garantindo consistência nas informações.
        /// </summary>
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
