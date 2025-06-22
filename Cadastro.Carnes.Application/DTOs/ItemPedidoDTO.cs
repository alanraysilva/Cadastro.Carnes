using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO de Item do Pedido.
    /// Representa cada linha de carne, quantidade e moeda dentro de um Pedido.
    /// </summary>
    public class ItemPedidoDTO
    {
        /// <summary>
        /// Identificador único do item do pedido.
        /// Preenchido automaticamente pelo banco.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Id do pedido ao qual este item pertence.
        /// Relaciona com PedidoDTO.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("PedidoId")]
        public int PedidoId { get; set; }

        /// <summary>
        /// Id da carne escolhida para este item.
        /// Relaciona com CarneDTO.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("CarneId")]
        public int CarneId { get; set; }

        /// <summary>
        /// Quantidade da carne neste item do pedido.
        /// Valor inteiro, obrigatório.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }

        /// <summary>
        /// Id da moeda em que o valor da carne está expresso.
        /// Relaciona com MoedaDTO.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("MoedaId")]
        public int MoedaId { get; set; }

        /// <summary>
        /// Valor unitário da carne neste item do pedido.
        /// Decimal com duas casas, obrigatório.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor")]
        public decimal Valor { get; set; }

        /// <summary>
        /// Objeto completo da carne (apenas para exibição).
        /// Não precisa ser enviado no POST/PUT.
        /// </summary>
        [ValidateNever]
        public CarneDTO? Carne { get; set; }

        /// <summary>
        /// Objeto completo da moeda (apenas para exibição).
        /// Não precisa ser enviado no POST/PUT.
        /// </summary>
        [ValidateNever]
        public MoedaDTO? Moeda { get; set; }

        /// <summary>
        /// Objeto completo do pedido (opcional, geralmente não utilizado em grid de itens).
        /// </summary>
        [ValidateNever]
        public PedidoDTO? Pedido { get; set; }
    }
}
