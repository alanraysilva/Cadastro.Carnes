using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO para Pedido.
    /// Representa o pedido realizado por um comprador, incluindo data, itens e total.
    /// </summary>
    public class PedidoDTO
    {
        /// <summary>
        /// Identificador único do pedido.
        /// Gerado automaticamente pelo banco de dados.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Data do pedido.
        /// Formato exibido: dd/MM/yyyy.
        /// Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data do Pedido")]
        public DateTime Data { get; set; }

        /// <summary>
        /// Id do comprador responsável pelo pedido.
        /// Relaciona com CompradorDTO.
        /// Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("CompradorId")]
        public int CompradorId { get; set; }

        /// <summary>
        /// Valor total do pedido em moeda corrente.
        /// Calculado no backend com base nos itens.
        /// Decimal com duas casas.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Objeto do comprador (preenchido apenas para exibição).
        /// Não precisa ser enviado no POST/PUT.
        /// </summary>
        [ValidateNever]
        public CompradorDTO? Comprador { get; set; }

        /// <summary>
        /// Lista de itens do pedido (cada carne, quantidade, valor, moeda).
        /// Preenchido na criação e exibição do pedido.
        /// </summary>
        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }
}
