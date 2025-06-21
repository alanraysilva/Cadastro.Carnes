using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class MoedaService : IMoedaService
    {
        private readonly IMoedaRepository _MoedaRepository;
        private readonly IItemPedidoRepository _ItemPedidoRepository;
        private readonly IMapper _mapper;

        public MoedaService(IMoedaRepository moedaRepository, IMapper mapper, IItemPedidoRepository itemPedidoRepository)
        {
            _MoedaRepository = moedaRepository;
            _ItemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(MoedaDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Moeda>(EnttiyDTO);
                await _MoedaRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        public async Task<IEnumerable<MoedaDTO>> GetAll()
        {
            var entity = await _MoedaRepository.GetAll();
            return _mapper.Map<IEnumerable<MoedaDTO>>(entity);
        }

        public async Task<MoedaDTO> GetById(int? id)
        {
            var entity = await _MoedaRepository.GetById(id);
            return _mapper.Map<MoedaDTO>(entity);
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var possui = await _ItemPedidoRepository.ExisteItemComEssaMoeda(id);

                if (possui != null)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Registro vinculado.");

                var entity = _MoedaRepository.GetById(id).Result;
                await _MoedaRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }

        }

        public async Task<RetornoPadraoDTO> Update(MoedaDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _MoedaRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Aplica as atualizações campo a campo ou usa o AutoMapper para mapear por cima:            
                _mapper.Map(EnttiyDTO, entityBanco); // Faz o overlay na entidade existente (rastreamento ok)
                await _MoedaRepository.Update(entityBanco);
                return new RetornoPadraoDTO(true, "Registro atualizado.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar atualizar registro: " + ex.Message);
            }
        }
    }
}
