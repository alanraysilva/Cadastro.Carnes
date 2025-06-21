using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class OrigemRepository : IOrigemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrigemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Origem> Create(Origem entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Origem> Delete(Origem entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Origem>> GetAll()
        {
            return await _context.Origem.ToListAsync();
        }

        public async Task<Origem> GetById(int? id)
        {
            var x = await _context.Origem.SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        public async Task<Origem> Update(Origem entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
