using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço responsável pela regra de negócio da entidade Cidade.
    /// Permite cadastrar, editar, excluir e consultar cidades.
    /// </summary>
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _CidadeRepository;
        private readonly ICompradorRepository _CompradorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injeta dependências necessárias: repositórios e mapper.
        /// </summary>
        public CidadeService(
            ICidadeRepository cidadeRepository,
            IMapper mapper,
            ICompradorRepository compradorRepository)
        {
            _CidadeRepository = cidadeRepository;
            _CompradorRepository = compradorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra uma nova cidade.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(CidadeDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Cidade>(EnttiyDTO); // Mapeia o DTO para entidade de domínio
                await _CidadeRepository.Create(entity); // Persiste no banco
                EnttiyDTO.Id = entity.Id; // Atualiza o DTO com o ID gerado
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        /// <summary>
        /// Retorna todas as cidades cadastradas.
        /// </summary>
        public async Task<IEnumerable<CidadeDTO>> GetAll()
        {
            var entity = await _CidadeRepository.GetAll();
            return _mapper.Map<IEnumerable<CidadeDTO>>(entity);
        }

        /// <summary>
        /// Busca uma cidade pelo ID.
        /// </summary>
        public async Task<CidadeDTO> GetById(int? id)
        {
            var entity = await _CidadeRepository.GetById(id);
            return _mapper.Map<CidadeDTO>(entity);
        }

        /// <summary>
        /// Remove uma cidade, validando se existe comprador vinculado.
        /// Só permite excluir se não houver nenhum comprador usando a cidade.
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Valida se algum comprador está vinculado a esta cidade
                var possuiComprador = await _CompradorRepository.ExisteCidadePorComprador(id);

                if (possuiComprador != null)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Cidade vinculada a comprador(es).");

                var entity = _CidadeRepository.GetById(id).Result; // Busca a cidade
                await _CidadeRepository.Delete(entity); // Remove
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma cidade existente.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(CidadeDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _CidadeRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrado.");

                // Atualiza os campos da entidade usando AutoMapper (overlay no objeto já rastreado)
                _mapper.Map(EnttiyDTO, entityBanco);
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
