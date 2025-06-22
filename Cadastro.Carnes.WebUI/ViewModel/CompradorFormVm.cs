using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class CompradorFormVm
    {
        // Lista de compradores para uso na interface de usuário
        public List<CompradorDTO> Compradores { get; set; } = new();

        // Lista de cidades para seleção ou exibição relacionada aos compradores
        public List<CidadeDTO> Cidades { get; set; } = new();
    }
}
