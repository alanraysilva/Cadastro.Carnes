using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço de regras de negócio para Origem.
    /// Responsável por operações CRUD e integridade relacionada às carnes.
    /// </summary>
    public class OrigemService : IOrigemService
    {
        // Repositório de origem (responsável por CRUD no banco)
        private readonly IOrigemRepository _OrigemRepository;
        // Repositório de carne, usado para validar vínculos antes da exclusão
        private readonly ICarneRepository _CarneRepository;
        // Mapper para facilitar conversão entre entidade e DTO (pra camada de apresentação)
        private readonly IMapper _mapper;

        /// <summary>
        /// Injeta dependências do service via construtor.
        /// </summary>
        public OrigemService(IOrigemRepository origemRepository, IMapper mapper, ICarneRepository carneRepository)
        {
            _OrigemRepository = origemRepository;
            _CarneRepository = carneRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma nova origem ao sistema.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(OrigemDTO EnttiyDTO)
        {
            try
            {
                // Converte o DTO recebido para entidade do domínio e salva
                var entity = _mapper.Map<Origem>(EnttiyDTO);
                await _OrigemRepository.Create(entity);
                EnttiyDTO.Id = entity.Id; // Garante retorno do ID para tela se precisar
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                // Retorna erro amigável, pronto pra interface mostrar pra pessoa
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        /// <summary>
        /// Lista todas as origens cadastradas.
        /// </summary>
        public async Task<IEnumerable<OrigemDTO>> GetAll()
        {
            var entity = await _OrigemRepository.GetAll();
            return _mapper.Map<IEnumerable<OrigemDTO>>(entity);
        }

        /// <summary>
        /// Busca origem pelo seu ID (usado em detalhes/edição).
        /// </summary>
        public async Task<OrigemDTO> GetById(int? id)
        {
            var entity = await _OrigemRepository.GetById(id);
            return _mapper.Map<OrigemDTO>(entity);
        }

        /// <summary>
        /// Remove uma origem se ela não estiver vinculada a nenhuma carne.
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Checa se existe alguma carne vinculada a essa origem.
                var possui = await _CarneRepository.ExisteCarneComEssaOrigem(id);

                if (possui != null)
                    // Se sim, não permite excluir e retorna mensagem
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Registro vinculado.");

                // Busca e remove a entidade (atenção ao .Result, refatorável pra await)
                var entity = _OrigemRepository.GetById(id).Result;
                await _OrigemRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma origem existente.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(OrigemDTO EnttiyDTO)
        {
            try
            {
                // Busca a origem pelo ID para garantir que existe
                var entityBanco = await _OrigemRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Usa AutoMapper para mapear o DTO por cima da entidade existente (tracking ok)
                _mapper.Map(EnttiyDTO, entityBanco);
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
