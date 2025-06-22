using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object para Carne.
    /// Usado para transferência de dados entre API, aplicação e views.
    /// </summary>
    public class CarneDTO
    {
        /// <summary>
        /// Identificador único da carne.
        /// Preenchido automaticamente pelo banco de dados.
        /// </summary>
        [DisplayName("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Nome descritivo da carne.
        /// Exemplo: "Picanha", "Alcatra".
        /// Obrigatório. Limite de 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Identificador da origem da carne.
        /// Refere-se ao Id de OrigemDTO (país, frigorífico, etc).
        /// Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("OrigemId")]
        public int OrigemId { get; set; }

        /// <summary>
        /// Objeto de navegação para exibição de dados da origem.
        /// Não deve ser preenchido em POST/PUT, apenas para consulta (GET).
        /// </summary>
        [ValidateNever]
        public OrigemDTO? Origem { get; set; }
    }
}
