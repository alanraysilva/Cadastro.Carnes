using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class CompradorService : ICompradorService
    {
        private readonly ICompradorRepository _CompradorRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public CompradorService(ICompradorRepository compradorRepository, IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _CompradorRepository = compradorRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(CompradorDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Comprador>(EnttiyDTO);
                await _CompradorRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        public async Task<IEnumerable<CompradorDTO>> GetAll()
        {
            var entity = await _CompradorRepository.GetAll();
            return _mapper.Map<IEnumerable<CompradorDTO>>(entity);
        }

        public async Task<CompradorDTO> GetById(int? id)
        {
            var entity = await _CompradorRepository.GetById(id);
            return _mapper.Map<CompradorDTO>(entity);
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var possuiPedido = await _pedidoRepository.ExistePedidoComVendedor(id);

                if (possuiPedido != null)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Ciadde vinculada a comprador(s).");

                var entity = _CompradorRepository.GetById(id).Result;
                await _CompradorRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        public async Task<RetornoPadraoDTO> Update(CompradorDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _CompradorRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Aplica as atualizações campo a campo ou usa o AutoMapper para mapear por cima:            
                _mapper.Map(EnttiyDTO, entityBanco); // Faz o overlay na entidade existente (rastreamento ok)
                await _CompradorRepository.Update(entityBanco);
                return new RetornoPadraoDTO(true, "Registro atualizado.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar atualizar registro: " + ex.Message);
            }

        }
    }
}
