using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class CarneFormVm
    {
        // Lista de carnes para exibição e manipulação na view
        public List<CarneDTO> Carnes { get; set; } = new List<CarneDTO>();

        // Lista de origens associadas às carnes para seleção ou exibição na view
        public List<OrigemDTO> Origem { get; set; } = new List<OrigemDTO>();
    }
}
