using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class ItemPedidoService : IItemPedidoService
    {
        private readonly IItemPedidoRepository _ItemPedidoRepository;
        private readonly IMapper _mapper;

        public ItemPedidoService(IItemPedidoRepository itemPedidoRepository, IMapper mapper)
        {
            _ItemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(ItemPedidoDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<ItemPedido>(EnttiyDTO);
                await _ItemPedidoRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }

        }

        public async Task<IEnumerable<ItemPedidoDTO>> GetAll()
        {
            var entity = await _ItemPedidoRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemPedidoDTO>>(entity);
        }

        public async Task<ItemPedidoDTO> GetById(int? id)
        {
            var entity = await _ItemPedidoRepository.GetById(id);
            return _mapper.Map<ItemPedidoDTO>(entity);
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var entity = _ItemPedidoRepository.GetById(id).Result;
                await _ItemPedidoRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }

        }

        public async Task<RetornoPadraoDTO> Update(ItemPedidoDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _ItemPedidoRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Aplica as atualizações campo a campo ou usa o AutoMapper para mapear por cima:            
                _mapper.Map(EnttiyDTO, entityBanco); // Faz o overlay na entidade existente (rastreamento ok)
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
