using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class PedidoFormVm
    {
        public List<PedidoDTO> Pedidos { get; set; } = new();
        public List<CompradorDTO> Compradores { get; set; } = new();
        public List<CarneDTO> Carnes { get; set; } = new();
        public List<MoedaDTO> Moedas { get; set; } = new(); // Se precisar!
    }
}
