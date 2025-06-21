using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Carnes.Application.DTOs
{
    public class MoedaDTO
    {

        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(10)]
        [DisplayName("Sigla")]
        public string Sigla { get; set; } = string.Empty;
    }
}
