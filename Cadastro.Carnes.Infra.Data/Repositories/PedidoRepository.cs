using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> Create(Pedido entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Pedido> Delete(Pedido entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Pedido> ExistePedidoComVendedor(int? compradorId)
        {
            var x = await _context.Pedido.Include(x => x.Comprador).SingleOrDefaultAsync(p => p.CompradorId == compradorId);
            return x!;
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            var pedidos = await _context.Pedido
                .Include(x => x.Comprador)
                .Include(x => x.Itens)
                    .ThenInclude(i => i.Carne)
                .Include(x => x.Itens)
                    .ThenInclude(i => i.Moeda)
                .AsNoTracking()
                .ToListAsync();

            // Remover referência cruzada
            foreach (var pedido in pedidos)
            {
                foreach (var item in pedido.Itens)
                    item.Pedido = null;
            }
            return pedidos;
        }

        public async Task<Pedido> GetById(int? id)
        {
            var pedidos = await _context.Pedido
                .Include(x => x.Comprador)
                .Include(x => x.Itens)
                    .ThenInclude(i => i.Carne)
                .Include(x => x.Itens)
                    .ThenInclude(i => i.Moeda).SingleOrDefaultAsync(p => p.Id == id);

            // Remover referência cruzada
            foreach (var item in pedidos!.Itens)
                item.Pedido = null;

            return pedidos ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        public async Task<Pedido> Update(Pedido entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
