using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class OrigemService : IOrigemService
    {
        private readonly IOrigemRepository _OrigemRepository;
        private readonly ICarneRepository _CarneRepository;
        private readonly IMapper _mapper;

        public OrigemService(IOrigemRepository origemRepository, IMapper mapper, ICarneRepository carneRepository)
        {
            _OrigemRepository = origemRepository;
            _CarneRepository = carneRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(OrigemDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Origem>(EnttiyDTO);
                await _OrigemRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        public async Task<IEnumerable<OrigemDTO>> GetAll()
        {
            var entity = await _OrigemRepository.GetAll();
            return _mapper.Map<IEnumerable<OrigemDTO>>(entity);
        }

        public async Task<OrigemDTO> GetById(int? id)
        {
            var entity = await _OrigemRepository.GetById(id);
            return _mapper.Map<OrigemDTO>(entity);
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var possui = await _CarneRepository.ExisteCarneComEssaOrigem(id);

                if (possui != null)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Registro vinculado.");

                var entity = _OrigemRepository.GetById(id).Result;
                await _OrigemRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        public async Task<RetornoPadraoDTO> Update(OrigemDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _OrigemRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Aplica as atualizações campo a campo ou usa o AutoMapper para mapear por cima:            
                _mapper.Map(EnttiyDTO, entityBanco); // Faz o overlay na entidade existente (rastreamento ok)
                await _OrigemRepository.Update(entityBanco);
                return new RetornoPadraoDTO(true, "Registro atualizado.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar atualizar registro: " + ex.Message);
            }
        }
    }
}
