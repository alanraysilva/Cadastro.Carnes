using Cadastro.Carnes.Domain.Entities;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Repositories
{
    public class CompradorRepository : ICompradorRepository
    {
        private ApplicationDbContext _context;

        public CompradorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comprador> Create(Comprador entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comprador> Delete(Comprador entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comprador> ExisteCidadePorComprador(int? cidadeId)
        {
            var x = await _context.Comprador.Include(x => x.Cidade).SingleOrDefaultAsync(p => p.CidadeId == cidadeId);
            return x!;
        }

        public async Task<IEnumerable<Comprador>> GetAll()
        {
            return await _context.Comprador.Include(x => x.Cidade).ToListAsync();
        }

        public async Task<Comprador> GetById(int? id)
        {
            var x = await _context.Comprador.Include(x => x.Cidade).SingleOrDefaultAsync(p => p.Id == id);

            return x ?? throw new KeyNotFoundException($"Registro não encontrado ID {id}");
        }

        public async Task<Comprador> Update(Comprador entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
    }
}
