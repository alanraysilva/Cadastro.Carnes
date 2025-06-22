using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Comprador.
    /// Representa uma pessoa física ou jurídica que pode realizar pedidos de carne.
    /// </summary>
    public class CompradorDTO
    {
        /// <summary>
        /// Identificador único do comprador.
        /// Preenchido pelo banco de dados.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do comprador (pessoa ou empresa).
        /// Exemplo: "João Silva" ou "Açougue São Pedro Ltda".
        /// Obrigatório. Máximo de 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Documento do comprador (CPF ou CNPJ).
        /// Deve ser apenas números (sem pontuação).
        /// Obrigatório. Máximo de 14 caracteres (11 para CPF, 14 para CNPJ).
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(14)]
        [DisplayName("CPF/CNPJ")]
        public string Documento { get; set; } = string.Empty;

        /// <summary>
        /// ID da cidade do comprador.
        /// Relaciona com a entidade CidadeDTO.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("CidadeId")]
        public int CidadeId { get; set; }

        /// <summary>
        /// Dados completos da cidade do comprador.
        /// Usado somente para exibição (não enviar em POST/PUT).
        /// </summary>
        [ValidateNever]
        public CidadeDTO? Cidade { get; set; }
    }
}
