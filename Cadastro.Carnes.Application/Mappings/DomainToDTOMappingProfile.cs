using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Carne, CarneDTO>().ReverseMap().IgnoreNavigation();
            CreateMap<Cidade, CidadeDTO>().ReverseMap();
            CreateMap<Comprador, CompradorDTO>().ReverseMap().IgnoreNavigation();
            CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap().IgnoreNavigation();
            CreateMap<Moeda, MoedaDTO>().ReverseMap();
            CreateMap<Origem, OrigemDTO>().ReverseMap();
            CreateMap<Pedido, PedidoDTO>().ReverseMap().IgnoreNavigation();
        }
    }
}
