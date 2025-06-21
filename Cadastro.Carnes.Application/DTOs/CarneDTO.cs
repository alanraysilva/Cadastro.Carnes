using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cadastro.Carnes.Application.DTOs
{
    public class CarneDTO
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("OrigemId")]
        public int OrigemId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public OrigemDTO? Origem { get; set; }
    }
}
