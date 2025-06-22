using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço de regras de negócio para ItemPedido.
    /// Responsável por cadastrar, editar, remover e listar itens de pedido.
    /// </summary>
    public class ItemPedidoService : IItemPedidoService
    {
        // Repositório específico para persistir/excluir/buscar itens do pedido
        private readonly IItemPedidoRepository _ItemPedidoRepository;

        // Responsável por mapear automaticamente entre DTO e entidade de domínio
        private readonly IMapper _mapper;

        /// <summary>
        /// Injeta dependências obrigatórias para o serviço.
        /// </summary>
        public ItemPedidoService(IItemPedidoRepository itemPedidoRepository, IMapper mapper)
        {
            _ItemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um novo item no pedido.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(ItemPedidoDTO EnttiyDTO)
        {
            try
            {
                // Converte DTO para entidade e salva no banco
                var entity = _mapper.Map<ItemPedido>(EnttiyDTO);
                await _ItemPedidoRepository.Create(entity);
                EnttiyDTO.Id = entity.Id; // Atualiza ID do DTO após inserir (caso precise exibir na tela)
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                // Retorna erro amigável pra UI exibir
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os itens de pedidos cadastrados.
        /// </summary>
        public async Task<IEnumerable<ItemPedidoDTO>> GetAll()
        {
            var entity = await _ItemPedidoRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemPedidoDTO>>(entity);
        }

        /// <summary>
        /// Busca um item de pedido pelo ID.
        /// </summary>
        public async Task<ItemPedidoDTO> GetById(int? id)
        {
            var entity = await _ItemPedidoRepository.GetById(id);
            return _mapper.Map<ItemPedidoDTO>(entity);
        }

        /// <summary>
        /// Remove um item do pedido pelo ID.
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Busca a entidade
                var entity = _ItemPedidoRepository.GetById(id).Result;
                await _ItemPedidoRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um item do pedido existente.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(ItemPedidoDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _ItemPedidoRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrado.");

                // Usa o AutoMapper para aplicar o overlay das alterações
                _mapper.Map(EnttiyDTO, entityBanco);
                await _ItemPedidoRepository.Update(entityBanco);
                return new RetornoPadraoDTO(true, "Registro atualizado.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar atualizar registro: " + ex.Message);
            }
        }
    }
}
