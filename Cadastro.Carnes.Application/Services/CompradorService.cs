using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço de regras de negócio para Comprador.
    /// Faz a ponte entre camada de aplicação e domínio, cuidando de validações e integrações.
    /// </summary>
    public class CompradorService : ICompradorService
    {
        // Repositório de comprador, para persistência.
        private readonly ICompradorRepository _CompradorRepository;

        // Repositório de pedido, usado para validar vínculo antes de excluir.
        private readonly IPedidoRepository _pedidoRepository;

        // AutoMapper para facilitar conversão entre DTO e entidade.
        private readonly IMapper _mapper;

        /// <summary>
        /// Injeta dependências essenciais para o serviço funcionar.
        /// </summary>
        public CompradorService(
            ICompradorRepository compradorRepository,
            IMapper mapper,
            IPedidoRepository pedidoRepository)
        {
            _CompradorRepository = compradorRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um novo comprador ao sistema.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(CompradorDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Comprador>(EnttiyDTO); // DTO para entidade
                await _CompradorRepository.Create(entity); // Salva no banco
                EnttiyDTO.Id = entity.Id; // Atualiza o DTO com o ID criado
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os compradores cadastrados.
        /// </summary>
        public async Task<IEnumerable<CompradorDTO>> GetAll()
        {
            var entity = await _CompradorRepository.GetAll();
            return _mapper.Map<IEnumerable<CompradorDTO>>(entity);
        }

        /// <summary>
        /// Busca um comprador pelo ID.
        /// </summary>
        public async Task<CompradorDTO> GetById(int? id)
        {
            var entity = await _CompradorRepository.GetById(id);
            return _mapper.Map<CompradorDTO>(entity);
        }

        /// <summary>
        /// Retorna o total de compradores ativos no sistema.
        /// </summary>
        public async Task<int> GetTotalAtivos()
        {
            var entity = await _CompradorRepository.GetAll();
            return entity.Count();
        }

        /// <summary>
        /// Remove um comprador, caso não exista pedido vinculado.
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Valida se existe algum pedido vinculado a este comprador
                var possuiPedido = await _pedidoRepository.ExistePedidoComVendedor(id);

                if (possuiPedido != null)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Pedidos vinculados ao(s) comprador(es).");

                var entity = _CompradorRepository.GetById(id).Result;
                await _CompradorRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um comprador existente.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(CompradorDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _CompradorRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrado.");

                // Atualiza os campos usando AutoMapper, respeitando rastreamento
                _mapper.Map(EnttiyDTO, entityBanco);
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
