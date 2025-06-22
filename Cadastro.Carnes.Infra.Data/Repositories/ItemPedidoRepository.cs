using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório responsável pela manipulação dos itens de pedido no banco de dados.
    /// Implementa operações CRUD e métodos auxiliares de consulta/exclusão.
    /// </summary>
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Construtor recebe o contexto do banco de dados por injeção de dependência.
        /// </summary>
        public ItemPedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo item de pedido ao banco de dados.
        /// </summary>
        public async Task<ItemPedido> Create(ItemPedido entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove todos os itens vinculados a um pedido específico.
        /// Retorna true se conseguiu excluir (ou se não tinha nada pra excluir), false se houve erro.
        /// </summary>
        public async Task<bool> DeletaItemPorNumeroDoPedido(int? pedidoId)
        {
            try
            {
                // Busca todos os itens do pedido de uma vez
                var itens = await _context.ItemPedido
                    .Where(p => p.PedidoId == pedidoId)
                    .ToListAsync();

                // Se não achou itens, não tem o que deletar
                if (!itens.Any())
                    return true;

                // Remove todos os itens encontrados de uma só vez
                _context.ItemPedido.RemoveRange(itens);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                // Em caso de erro, retorna false (poderia logar o erro aqui)
                return false;
            }
        }

        /// <summary>
        /// Remove um item de pedido específico do banco de dados.
        /// </summary>
        public async Task<ItemPedido> Delete(ItemPedido entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Retorna o primeiro item de pedido que utiliza a moeda informada.
        /// </summary>
        public async Task<ItemPedido> ExisteItemComEssaMoeda(int? moedaId)
        {
            var x = await _context.ItemPedido.SingleOrDefaultAsync(p => p.MoedaId == moedaId);
            return x!;
        }

        /// <summary>
        /// Retorna a lista de itens de pedido vinculados a uma carne específica.
        /// </summary>
        public async Task<List<ItemPedido>> ExistePorCarneIdAsync(int? carneId)
        {
            var x = await _context.ItemPedido.Where(p => p.CarneId == carneId).ToListAsync();
            return x!;
        }

        /// <summary>
        /// Lista todos os itens de pedido, incluindo carne e moeda relacionadas.
        /// </summary>
        public async Task<IEnumerable<ItemPedido>> GetAll()
        {
            return await _context.ItemPedido
                                 .Include(x => x.Carne)
                                 .Include(x => x.Moeda)
                                 .ToListAsync();
        }

        /// <summary>
        /// Busca um item de pedido pelo Id, incluindo carne e moeda relacionadas.
        /// Lança exceção se não encontrar.
        /// </summary>
        public async Task<ItemPedido> GetById(int? id)
        {
            var x = await _context.ItemPedido
                                  .Include(x => x.Carne)
                                  .Include(x => x.Moeda)
                                  .SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza um item de pedido existente no banco de dados.
        /// </summary>
        public async Task<ItemPedido> Update(ItemPedido entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
