using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    /// <summary>
    /// Serviço responsável pela lógica de negócio de Carnes.
    /// Implementa métodos CRUD e outras regras do domínio.
    /// </summary>
    public class CarneService : ICarneService
    {
        private readonly ICarneRepository _carneRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injeta dependências via construtor (repositórios e AutoMapper).
        /// </summary>
        public CarneService(ICarneRepository carneRepository, IMapper mapper, IItemPedidoRepository itemPedidoRepository)
        {
            _carneRepository = carneRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma nova carne ao banco de dados.
        /// </summary>
        public async Task<RetornoPadraoDTO> Add(CarneDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Carne>(EnttiyDTO); // Mapeia DTO para entidade
                await _carneRepository.Create(entity);
                EnttiyDTO.Id = entity.Id; // Retorna o ID gerado
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        /// <summary>
        /// Retorna todas as carnes cadastradas.
        /// </summary>
        public async Task<IEnumerable<CarneDTO>> GetAll()
        {
            var entity = await _carneRepository.GetAll();
            return _mapper.Map<IEnumerable<CarneDTO>>(entity);
        }

        /// <summary>
        /// Busca carne pelo ID.
        /// </summary>
        public async Task<CarneDTO> GetById(int? id)
        {
            var entity = await _carneRepository.GetById(id);
            return _mapper.Map<CarneDTO>(entity);
        }

        /// <summary>
        /// Retorna o total de carnes cadastradas.
        /// </summary>
        public async Task<int> GetTotalCount()
        {
            var entity = await _carneRepository.GetAll();
            return entity.Count();
        }

        /// <summary>
        /// Remove uma carne, se não estiver vinculada a nenhum pedido.
        /// </summary>
        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                // Checa se existe item de pedido vinculado à carne antes de excluir
                var possuiPedidos = await _itemPedidoRepository.ExistePorCarneIdAsync(id);

                if (possuiPedidos?.Count > 0)
                    return new RetornoPadraoDTO(false, "Não é possível excluir. Carne vinculada a pedidos.");

                var entity = _carneRepository.GetById(id).Result;
                await _carneRepository.Delete(entity);
                return new RetornoPadraoDTO(true, "Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar excluir registro: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma carne.
        /// </summary>
        public async Task<RetornoPadraoDTO> Update(CarneDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _carneRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null)
                    return new RetornoPadraoDTO(false, "Registro não encontrado.");

                // Aplica as mudanças do DTO para a entidade já rastreada pelo contexto
                _mapper.Map(EnttiyDTO, entityBanco);
                await _carneRepository.Update(entityBanco);
                return new RetornoPadraoDTO(true, "Registro atualizado.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(true, "Erro ao tentar atualizar registro: " + ex.Message);
            }
        }
    }
}
