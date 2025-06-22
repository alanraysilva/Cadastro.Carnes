using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private ApplicationDbContext _context;

        public ItemPedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ItemPedido> Create(ItemPedido entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeletaItemPorNumeroDoPedido(int? pedidoId)
        {
            try
            {
                var x = await _context.ItemPedido.Where(p => p.PedidoId == pedidoId).ToListAsync();
                foreach (var item in x)
                {
                    await Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ItemPedido> Delete(ItemPedido entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ItemPedido> ExisteItemComEssaMoeda(int? moedaId)
        {
            var x = await _context.ItemPedido.SingleOrDefaultAsync(p => p.MoedaId == moedaId);
            return x!;
        }

        public async Task<ItemPedido> ExistePorCarneIdAsync(int? carneId)
        {
            var x = await _context.ItemPedido.SingleOrDefaultAsync(p => p.CarneId == carneId);
            return x!;
        }

        public async Task<IEnumerable<ItemPedido>> GetAll()
        {
            return await _context.ItemPedido.Include(x => x.Carne).Include(x => x.Moeda).ToListAsync();
        }

        public async Task<ItemPedido> GetById(int? id)
        {
            var x = await _context.ItemPedido.Include(x => x.Carne).Include(x => x.Moeda).SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        public async Task<ItemPedido> Update(ItemPedido entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
