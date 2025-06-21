using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cadastro.Carnes.Application.DTOs
{
    public class ItemPedidoDTO
    {

        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("PedidoId")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("CarneId")]
        public int CarneId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("MoedaId")]
        public int MoedaId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor")]
        public decimal Valor { get; set; }


        [JsonIgnore]
        [ValidateNever]
        public CarneDTO? Carne { get; set; }


        [JsonIgnore]
        [ValidateNever]
        public MoedaDTO? Moeda { get; set; }


        [JsonIgnore]
        [ValidateNever]
        public PedidoDTO? Pedido { get; set; }
    }
}
