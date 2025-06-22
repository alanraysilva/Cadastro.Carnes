using Cadastro.Carnes.Domain.Entities;

namespace Cadastro.Carnes.Infra.Data.Context
{
    /// <summary>
    /// Classe utilitária responsável por popular o banco de dados com dados iniciais (seed).
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Realiza o seed das tabelas essenciais no banco (Origem e Moeda), caso estejam vazias.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public static void Seed(ApplicationDbContext context)
        {
            // Garante que o banco de dados está criado antes de inserir dados
            context.Database.EnsureCreated();

            // Verifica se já existem registros na tabela Origem
            if (!context.Origem.Any())
            {
                // Adiciona origens padrão para carnes
                context.Origem.AddRange(
                    new Origem("Bovina"),
                    new Origem("Suína"),
                    new Origem("Aves"),
                    new Origem("Peixes")
                );
            }

            // Verifica se já existem registros na tabela Moeda
            if (!context.Moeda.Any())
            {
                // Adiciona moedas padrão utilizadas no sistema
                context.Moeda.AddRange(
                    new Moeda("Real", "BRL"),
                    new Moeda("Dólar", "USD"),
                    new Moeda("Euro", "EUR")
                );
            }

            // Salva as alterações no banco de dados, caso necessário
            context.SaveChanges();
        }
    }
}
