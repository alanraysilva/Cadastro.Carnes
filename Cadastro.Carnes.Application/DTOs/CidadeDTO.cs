using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Cidade entre camadas.
    /// Representa uma cidade brasileira com UF em formato de sigla.
    /// </summary>
    public class CidadeDTO
    {
        /// <summary>
        /// Identificador único da cidade.
        /// Preenchido pelo banco ao criar um novo registro.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome completo da cidade.
        /// Exemplo: "Campinas", "Porto Alegre".
        /// Obrigatório. Limitado a 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Sigla do estado da cidade (UF).
        /// Exemplo: "SP", "RS", "MG".
        /// Sempre dois caracteres, obrigatório.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(2)]
        [DisplayName("Estado")]
        public string Estado { get; set; } = string.Empty;
    }
}
