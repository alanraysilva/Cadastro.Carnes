using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cadastro.Carnes.Application.DTOs
{
    public class CompradorDTO
    {
     
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(14)]
        [DisplayName("CPF/CNPJ")]
        public string Documento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("CidadeId")]
        public int CidadeId { get; set; }


        [JsonIgnore]
        [ValidateNever]
        public CidadeDTO? Cidade { get; set; }
    }
}
