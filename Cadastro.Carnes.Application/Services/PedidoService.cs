using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _PedidoRepository;
        private readonly IItemPedidoRepository _IItemPedidoRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository, IMapper mapper, IItemPedidoRepository itemPedidoRepository)
        {
            _PedidoRepository = pedidoRepository;
            _IItemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(PedidoDTO EnttiyDTO)
        {
            try
            {
                if (EnttiyDTO.Itens == null || !EnttiyDTO.Itens.Any())
                    return new RetornoPadraoDTO(false, "O pedido deve conter ao menos um item.");

                // Converte os itens manualmente
                var itens = EnttiyDTO.Itens.Select(i =>
                    new ItemPedido(i.PedidoId,i.CarneId, i.Quantidade, i.MoedaId, i.Valor)).ToList();

                // Usa o construtor correto do domínio para garantir validação
                var pedido = new Pedido(EnttiyDTO.Data, EnttiyDTO.CompradorId, itens);

                await _PedidoRepository.Create(pedido);

                // Retorna o ID criado no DTO, se necessário
                EnttiyDTO.Id = pedido.Id;

                return new RetornoPadraoDTO(true, "Pedido cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar: " + ex.Message);
            }

        }

        public async Task<IEnumerable<PedidoDTO>> GetAll()
        {
            var entity = await _PedidoRepository.GetAll();
            return _mapper.Map<IEnumerable<PedidoDTO>>(entity);
        }

        public async Task<PedidoDTO> GetById(int? id)
        {
            var entity = await _PedidoRepository.GetById(id);
            return _mapper.Map<PedidoDTO>(entity);
        }

        public async Task<int> GetTotalCount()
        {
            var entity = await _PedidoRepository.GetAll();
            return entity.Count();
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var entity = _PedidoRepository.GetById(id).Result;
                var deletou = await _IItemPedidoRepository.DeletaItemPorNumeroDoPedido(id);
                if (!deletou) new RetornoPadraoDTO(false, "Não é possível excluir. Registro vinculado.");
                await _PedidoRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }

        }

        public async Task<RetornoPadraoDTO> Update(PedidoDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _PedidoRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrado.");

                // Monta nova lista de itens com base no DTO
                var novosItens = EnttiyDTO.Itens.Select(i =>
                    new ItemPedido(i.PedidoId, i.CarneId, i.Quantidade, i.MoedaId, i.Valor)).ToList();

                // Atualiza o domínio com segurança
                entityBanco.Update(EnttiyDTO.Data, EnttiyDTO.CompradorId, novosItens);

                await _PedidoRepository.Update(entityBanco);

                return new RetornoPadraoDTO(true, "Registro atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar atualizar registro: " + ex.Message);
            }

        }
    }
}
