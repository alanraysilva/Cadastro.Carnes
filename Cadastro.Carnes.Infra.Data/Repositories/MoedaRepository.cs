using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class MoedaRepository : IMoedaRepository
    {
        private ApplicationDbContext _context;

        public MoedaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Moeda> Create(Moeda entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Moeda> Delete(Moeda entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Moeda>> GetAll()
        {
            return await _context.Moeda.ToListAsync();
        }

        public async Task<Moeda> GetById(int? id)
        {
            var x = await _context.Moeda.SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        public async Task<Moeda> Update(Moeda entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
