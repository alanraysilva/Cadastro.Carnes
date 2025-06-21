using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cadastro.Carnes.Application.DTOs
{
    public class PedidoDTO
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data do Pedido")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("CompradorId")]
        public int CompradorId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public CompradorDTO? Comprador { get; set; }

        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }
}
