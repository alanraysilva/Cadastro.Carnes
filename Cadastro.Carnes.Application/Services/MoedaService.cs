using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço de regras de negócio para Moeda.
    /// Controla operações CRUD, valida dependências e aplica regras para evitar exclusão de moedas em uso.
    /// </summary>
    public class MoedaService : IMoedaService
    {
        // Repositório responsável pela persistência das moedas (CRUD banco)
        private readonly IMoedaRepository _MoedaRepository;
        // Repositório dos itens do pedido, usado para impedir exclusão de moeda usada em pedidos
        private readonly IItemPedidoRepository _ItemPedidoRepository;
        // Responsável por mapear entre entidade e DTO (camada de apresentação)
        private readonly IMapper _mapper;

        /// <summary>
        /// Injeta dependências via construtor (Dependency Injection)
        /// </summary>
        public MoedaService(IMoedaRepository moedaRepository, IMapper mapper, IItemPedidoRepository itemPedidoRepository)
        {
            _MoedaRepository = moedaRepository;
            _ItemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma nova moeda ao sistema.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(MoedaDTO EnttiyDTO)
        {
            try
            {
                // Converte DTO para entidade, salva e retorna ID gerado
                var entity = _mapper.Map<Moeda>(EnttiyDTO);
                await _MoedaRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                // Retorna erro amigável para interface
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        /// <summary>
        /// Lista todas as moedas cadastradas no sistema.
        /// </summary>
        public async Task<IEnumerable<MoedaDTO>> GetAll()
        {
            var entity = await _MoedaRepository.GetAll();
            return _mapper.Map<IEnumerable<MoedaDTO>>(entity);
        }

        /// <summary>
        /// Busca moeda por ID.
        /// </summary>
        public async Task<MoedaDTO> GetById(int? id)
        {
            var entity = await _MoedaRepository.GetById(id);
            return _mapper.Map<MoedaDTO>(entity);
        }

        /// <summary>
        /// Remove uma moeda do sistema, desde que não esteja vinculada a itens de pedido.
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Checa se existe algum item de pedido usando essa moeda
                var possui = await _ItemPedidoRepository.ExisteItemComEssaMoeda(id);

                if (possui != null)
                    // Não deixa excluir moeda já usada, regra de negócio clássica!
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Registro vinculado.");

                // Busca e remove a entidade (lembrar que GetById.Result pode travar thread; pode refatorar depois)
                var entity = _MoedaRepository.GetById(id).Result;
                await _MoedaRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma moeda existente.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(MoedaDTO EnttiyDTO)
        {
            try
            {
                // Busca moeda no banco para garantir que existe
                var entityBanco = await _MoedaRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Usa AutoMapper pra atualizar os campos, mantendo tracking da entidade
                _mapper.Map(EnttiyDTO, entityBanco);
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
