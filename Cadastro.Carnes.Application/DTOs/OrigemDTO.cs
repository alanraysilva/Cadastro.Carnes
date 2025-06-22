using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO de Origem.
    /// Representa a origem da carne cadastrada (Exemplo: país, estado, frigorífico, fornecedor).
    /// </summary>
    public class OrigemDTO
    {
        /// <summary>
        /// Identificador único da origem.
        /// Preenchido automaticamente pelo banco.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome descritivo da origem.
        /// Exemplo: "Brasil", "Uruguai", "Frigorífico Boa Carne".
        /// Obrigatório. Até 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;
    }
}
