using AutoMapper;
using Cadastro.Carnes.Application.DTOs;
using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;

namespace Cadastro.Carnes.Application.Services
{
    public class CarneService : ICarneService
    {
        private readonly ICarneRepository _carneRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IMapper _mapper;

        public CarneService(ICarneRepository carneRepository, IMapper mapper, IItemPedidoRepository itemPedidoRepository)
        {
            _carneRepository = carneRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        public async Task<RetornoPadraoDTO> Add(CarneDTO EnttiyDTO)
        {
            try
            {
                var entity = _mapper.Map<Carne>(EnttiyDTO);
                await _carneRepository.Create(entity);
                EnttiyDTO.Id = entity.Id;
                return new RetornoPadraoDTO(true, "Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new RetornoPadraoDTO(false, "Erro ao tentar cadastrar :" + ex.Message);
            }
        }

        public async Task<IEnumerable<CarneDTO>> GetAll()
        {
            var entity = await _carneRepository.GetAll();
            return _mapper.Map<IEnumerable<CarneDTO>>(entity);
        }

        public async Task<CarneDTO> GetById(int? id)
        {
            var entity = await _carneRepository.GetById(id);
            return _mapper.Map<CarneDTO>(entity);
        }

        public async Task<int> GetTotalCount()
        {
            var entity = await _carneRepository.GetAll();
            return entity.Count();
        }

        public async Task<RetornoPadraoDTO> Remove(int? id)
        {
            try
            {
                var possuiPedidos = await _itemPedidoRepository.ExistePorCarneIdAsync(id);

                if (possuiPedidos != null)
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

        public async Task<RetornoPadraoDTO> Update(CarneDTO EnttiyDTO)
        {
            try
            {
                var entityBanco = await _carneRepository.GetById(EnttiyDTO.Id);
                if (entityBanco == null) return new RetornoPadraoDTO(false, "Registro não encontrada.");

                // Aplica as atualizações campo a campo ou usa o AutoMapper para mapear por cima:            
                _mapper.Map(EnttiyDTO, entityBanco); // Faz o overlay na entidade existente (rastreamento ok)
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
