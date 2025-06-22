using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class CidadeFormVm
    {
        // Lista de cidades para exibição e manipulação na view
        public List<CidadeDTO> Cidades { get; set; } = new();
    }
}
