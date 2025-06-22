using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Carnes.Application.DTOs
{
    /// <summary>
    /// DTO de Moeda.
    /// Representa uma moeda aceita no sistema (ex: Real, Dólar, Euro).
    /// </summary>
    public class MoedaDTO
    {
        /// <summary>
        /// Identificador único da moeda.
        /// Preenchido pelo banco de dados.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome completo da moeda.
        /// Exemplo: "Real Brasileiro", "Dólar Americano".
        /// Obrigatório. Até 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Sigla padrão da moeda conforme mercado financeiro.
        /// Exemplo: "BRL", "USD", "EUR".
        /// Obrigatório. Até 10 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(10)]
        [DisplayName("Sigla")]
        public string Sigla { get; set; } = string.Empty;
    }
}
