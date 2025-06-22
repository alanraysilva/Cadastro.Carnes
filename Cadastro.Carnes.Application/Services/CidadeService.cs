using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _CidadeRepository;
        private readonly ICompradorRepository _CompradorRepository;
        private readonly IMapper _mapper;

        public CidadeService(ICidadeRepository cidadeRepository, IMapper mapper, ICompradorRepository compradorRepository)
        {
            _CidadeRepository = cidadeRepository;
            _CompradorRepository = compradorRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(CidadeDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Cidade>(EnttiyDTO);
                await _CidadeRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);

            }
        }

        public async Task<IEnumerable<CidadeDTO>> GetAll()
        {
            var entity = await _CidadeRepository.GetAll();
            return _mapper.Map<IEnumerable<CidadeDTO>>(entity);
        }

        public async Task<CidadeDTO> GetById(int? id)
        {
            var entity = await _CidadeRepository.GetById(id);
            return _mapper.Map<CidadeDTO>(entity);
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var possuiComprador = await _CompradorRepository.ExisteCidadePorComprador(id);

                if (possuiComprador != null)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Ciadade vinculada a comprador(es).");

                var entity = _CidadeRepository.GetById(id).Result;
                await _CidadeRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");

            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }

        }

        public async Task<RetornoPadraoDTO> Update(CidadeDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _CidadeRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Aplica as atualizações campo a campo ou usa o AutoMapper para mapear por cima:            
                _mapper.Map(EnttiyDTO, entityBanco); // Faz o overlay na entidade existente (rastreamento ok)
                await _CidadeRepository.Update(entityBanco);
                return new RetornoPadraoDTO(true, "Registro atualizado.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar atualizar registro: " + ex.Message);
            }

        }
    }
}
