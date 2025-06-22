using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class CompradorFormVm
    {
        public List<CompradorDTO> Compradores { get; set; } = new();
        public List<CidadeDTO> Cidades { get; set; } = new();
    }
}
