using Cadastro.Carnes.Application.DTOs;

namespace Cadastro.Carnes.WebUI.ViewModel
{
    public class PedidoFormVm
    {
        // Lista de pedidos para exibição ou manipulação na interface.
        public List<PedidoDTO> Pedidos { get; set; } = new();

        // Lista de compradores para associação com pedidos
        public List<CompradorDTO> Compradores { get; set; } = new();

        // Lista de carnes disponíveis para seleção nos pedidos
        public List<CarneDTO> Carnes { get; set; } = new();

        // Lista de moedas para uso em itens de pedido, caso necessário
        public List<MoedaDTO> Moedas { get; set; } = new();
    }
}
