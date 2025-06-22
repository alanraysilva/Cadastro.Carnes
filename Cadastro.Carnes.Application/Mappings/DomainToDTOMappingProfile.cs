using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Application.Mappings
{
    /// <summary>
    /// Perfil de configuração do AutoMapper para mapear entidades do domínio para seus respectivos DTOs e vice-versa.
    /// </summary>
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            // Mapeamento entre Carne e CarneDTO (bidirecional)
            // .IgnoreNavigation() evita que propriedades de navegação causem ciclos ou problemas de referência.
            CreateMap<Carne, CarneDTO>().ReverseMap().IgnoreNavigation();

            // Mapeamento entre Cidade e CidadeDTO (bidirecional)
            CreateMap<Cidade, CidadeDTO>().ReverseMap();

            // Mapeamento entre Comprador e CompradorDTO (bidirecional)
            // .IgnoreNavigation() para não mapear navegações do domínio
            CreateMap<Comprador, CompradorDTO>().ReverseMap().IgnoreNavigation();

            // Mapeamento entre ItemPedido e ItemPedidoDTO (bidirecional)
            // Ignora navegação para evitar ciclos e redundância
            CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap().IgnoreNavigation();

            // Mapeamento entre Moeda e MoedaDTO (bidirecional)
            CreateMap<Moeda, MoedaDTO>().ReverseMap();

            // Mapeamento entre Origem e OrigemDTO (bidirecional)
            CreateMap<Origem, OrigemDTO>().ReverseMap();

            // Mapeamento entre Pedido e PedidoDTO (bidirecional)
            // Ignora navegação para evitar recursividade
            CreateMap<Pedido, PedidoDTO>().ReverseMap().IgnoreNavigation();
        }
    }
}
