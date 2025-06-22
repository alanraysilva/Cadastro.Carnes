using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço responsável pelas regras de negócio do Pedido.
    /// Gerencia a vida do pedido: cadastro, consulta, atualização e remoção, sempre garantindo integridade dos itens.
    /// </summary>
    public class PedidoService : IPedidoService
    {
        // Repositório de pedido (CRUD na tabela de pedidos)
        private readonly IPedidoRepository _PedidoRepository;
        // Repositório de itens de pedido (CRUD nos itens e verificação de vínculos)
        private readonly IItemPedidoRepository _IItemPedidoRepository;
        // Mapper para transformar entre entidade e DTO (pra não enlouquecer de tanto "copy-paste")
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor recebe todos os repositórios e o mapper necessários
        /// </summary>
        public PedidoService(IPedidoRepository pedidoRepository, IMapper mapper, IItemPedidoRepository itemPedidoRepository)
        {
            _PedidoRepository = pedidoRepository;
            _IItemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um novo pedido. Valida se há itens e monta os objetos de domínio certinho.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(PedidoDTO EnttiyDTO)
        {
            try
            {
                // Validação de negócio: pedido precisa de pelo menos um item!
                if (EnttiyDTO.Itens == null || !EnttiyDTO.Itens.Any())
                    return new RetornoPadraoDTO(false, "O pedido deve conter ao menos um item.");

                // Cria os itens de domínio usando o construtor correto (validando business rules)
                var itens = EnttiyDTO.Itens.Select(i =>
                    new ItemPedido(i.PedidoId, i.CarneId, i.Quantidade, i.MoedaId, i.Valor)).ToList();

                // Cria o pedido do domínio (passa por todas as validações da entidade)
                var pedido = new Pedido(EnttiyDTO.Data, EnttiyDTO.CompradorId, itens);

                // Salva no banco via repositório
                await _PedidoRepository.Create(pedido);

                // Retorna o ID novo para quem precisar na camada superior
                EnttiyDTO.Id = pedido.Id;

                return new RetornoPadraoDTO(true, "Pedido cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                // Retorna mensagem de erro detalhada para a interface exibir bonitinho
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os pedidos cadastrados, incluindo seus itens (mapeados para DTO)
        /// </summary>
        public async Task<IEnumerable<PedidoDTO>> GetAll()
        {
            var entity = await _PedidoRepository.GetAll();
            return _mapper.Map<IEnumerable<PedidoDTO>>(entity);
        }

        /// <summary>
        /// Busca um pedido específico pelo ID, já convertido pra DTO pra facilitar a vida do front
        /// </summary>
        public async Task<PedidoDTO> GetById(int? id)
        {
            var entity = await _PedidoRepository.GetById(id);
            return _mapper.Map<PedidoDTO>(entity);
        }

        /// <summary>
        /// Retorna o total de pedidos cadastrados (útil pra estatísticas e dashboard)
        /// </summary>
        public async Task<int> GetTotalCount()
        {
            var entity = await _PedidoRepository.GetAll();
            return entity.Count();
        }

        /// <summary>
        /// Remove um pedido, mas antes de excluir, apaga todos os itens vinculados a ele (integridade sempre!)
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Busca o pedido primeiro
                var entity = _PedidoRepository.GetById(id).Result;

                // Remove todos os itens desse pedido (garante que não fica nada órfão)
                var deletou = await _IItemPedidoRepository.DeletaItemPorNumeroDoPedido(id);

                // Se não conseguiu deletar os itens, retorna erro
                if (!deletou) new RetornoPadraoDTO(false, "Não é possível excluir. Registro vinculado.");

                // Agora pode remover o pedido principal
                await _PedidoRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                // Retorna erro padrão pra tela não ficar muda
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um pedido existente. Atualiza todos os campos e substitui os itens conforme o DTO novo.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(PedidoDTO EnttiyDTO)
        {
            try
            {
                // Busca o pedido do banco pra atualizar sobre o objeto existente (tracking do EF Core)
                var entityBanco = await _PedidoRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrado.");

                // Cria nova lista de itens de pedido (garantindo integridade e regrinhas do domínio)
                var novosItens = EnttiyDTO.Itens.Select(i =>
                    new ItemPedido(i.PedidoId, i.CarneId, i.Quantidade, i.MoedaId, i.Valor)).ToList();

                // Chama método do domínio que atualiza todos os campos importantes (segurança total)
                entityBanco.Update(EnttiyDTO.Data, EnttiyDTO.CompradorId, novosItens);

                await _PedidoRepository.Update(entityBanco);

                return new RetornoPadraoDTO(true, "Registro atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                // Manda mensagem de erro pra interface exibir
                return new RetornoPadraoDTO(false, "Erro ao tentar atualizar registro: " + ex.Message);
            }
        }
    }
}
