using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class CarneRepository : ICarneRepository
    {
        private ApplicationDbContext _context;

        public CarneRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Carne> Create(Carne entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Carne> Delete(Carne entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Carne> ExisteCarneComEssaOrigem(int? origemId)
        {
            var x = await _context.Carne.SingleOrDefaultAsync(p => p.OrigemId == origemId);

            return x!;
        }

        public async Task<IEnumerable<Carne>> GetAll()
        {
            return await _context.Carne.Include(x => x.Origem).ToListAsync();
        }

        public async Task<Carne> GetById(int? id)
        {
            var x = await _context.Carne.Include(x => x.Origem).SingleOrDefaultAsync(p => p.Id== id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        public async Task<Carne> Update(Carne entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
