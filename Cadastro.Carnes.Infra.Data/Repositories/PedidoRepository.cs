using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de pedidos, responsável por manipular dados da entidade Pedido no banco.
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        // Contexto do banco de dados utilizado para as operações
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor recebe o contexto via injeção de dependência.
        /// </summary>
        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo pedido no banco de dados.
        /// </summary>
        public async Task<Pedido> Create(Pedido entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove um pedido existente do banco de dados.
        /// </summary>
        public async Task<Pedido> Delete(Pedido entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Busca um pedido que tenha um determinado comprador vinculado.
        /// Retorna o pedido encontrado ou null se não existir.
        /// </summary>
        public async Task<Pedido> ExistePedidoComVendedor(int? compradorId)
        {
            var x = await _context.Pedido
                .Include(x => x.Comprador)
                .SingleOrDefaultAsync(p => p.CompradorId == compradorId);
            return x!;
        }

        /// <summary>
        /// Retorna todos os pedidos cadastrados, incluindo comprador, itens, carne e moeda.
        /// Remove referência cruzada para evitar loop em serialização.
        /// </summary>
        public async Task<IEnumerable<Pedido>> GetAll()
        {
            var pedidos = await _context.Pedido
                .Include(x => x.Comprador)
                .Include(x => x.Itens).ThenInclude(i => i.Carne)
                .Include(x => x.Itens).ThenInclude(i => i.Moeda)
                .AsNoTracking()
                .ToListAsync();

            // Remove a referência cruzada para evitar ciclos em serialização JSON
            foreach (var pedido in pedidos)
            {
                foreach (var item in pedido.Itens)
                    item.Pedido = null;
            }
            return pedidos;
        }

        /// <summary>
        /// Busca um pedido pelo Id, incluindo comprador, itens, carne e moeda.
        /// Remove referência cruzada antes de retornar.
        /// </summary>
        public async Task<Pedido> GetById(int? id)
        {
            var pedidos = await _context.Pedido
                .Include(x => x.Comprador)
                .Include(x => x.Itens).ThenInclude(i => i.Carne)
                .Include(x => x.Itens).ThenInclude(i => i.Moeda)
                .SingleOrDefaultAsync(p => p.Id == id);

            // Remove referência cruzada, se encontrado
            foreach (var item in pedidos!.Itens)
                item.Pedido = null;

            return pedidos ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        /// <summary>
        /// Atualiza os dados de um pedido existente no banco de dados.
        /// </summary>
        public async Task<Pedido> Update(Pedido entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
