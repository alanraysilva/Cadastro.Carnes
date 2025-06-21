using Cadastro.Carnes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Carnes.Infra.Data.Context
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Origem.Any())
            {
                context.Origem.AddRange(
                    new Origem("Bovina"),
                    new Origem("Suína"),
                    new Origem("Aves"),
                    new Origem("Peixes")
                );
            }

            if (!context.Moeda.Any())
            {
                context.Moeda.AddRange(
                    new Moeda("Real", "BRL"),
                    new Moeda("Dólar", "USD"),
                    new Moeda("Euro", "EUR")
                );
            }

            context.SaveChanges();
        }
    }
}
