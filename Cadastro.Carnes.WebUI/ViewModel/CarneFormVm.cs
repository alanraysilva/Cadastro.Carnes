using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class CarneFormVm
    {
        public List<CarneDTO> Carnes { get; set; } = new List<CarneDTO>();
        public List<OrigemDTO> Origem { get; set; } = new List<OrigemDTO>();
    }
}
