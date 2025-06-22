using Cadastro.Carnes.Application.Interfaces;
using Cadastro.Carnes.Application.Mappings;
using Cadastro.Carnes.Application.Services;
using Cadastro.Carnes.Domain.Interface;
using Cadastro.Carnes.Infra.Data.Context;
using Cadastro.Carnes.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadastro.Carnes.Infra.IoC
{
    /// <summary>
    /// Classe responsável por registrar todas as dependências do sistema no container de injeção de dependência.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Método de extensão para adicionar e configurar as dependências da infraestrutura.
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura o contexto do Entity Framework apontando para o SQL Server, usando a connection string definida no appsettings.json
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            // Registra os repositórios e serviços de Carne (escopo por request)
            services.AddScoped<ICarneRepository, CarneRepository>();
            services.AddScoped<ICarneService, CarneService>();

            // Registra os repositórios e serviços de Cidade
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<ICidadeService, CidadeService>();

            // Registra os repositórios e serviços de Comprador
            services.AddScoped<ICompradorRepository, CompradorRepository>();
            services.AddScoped<ICompradorService, CompradorService>();

            // Registra os repositórios e serviços de ItemPedido
            services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
            services.AddScoped<IItemPedidoService, ItemPedidoService>();

            // Registra os repositórios e serviços de Moeda
            services.AddScoped<IMoedaRepository, MoedaRepository>();
            services.AddScoped<IMoedaService, MoedaService>();

            // Registra os repositórios e serviços de Origem
            services.AddScoped<IOrigemRepository, OrigemRepository>();
            services.AddScoped<IOrigemService, OrigemService>();

            // Registra os repositórios e serviços de Pedido
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoService, PedidoService>();

            // Registra a implementação de UnitOfWork para controle transacional
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            // Configura o AutoMapper com o perfil de mapeamento definido
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            // Retorna o IServiceCollection para permitir chamadas encadeadas
            return services;
        }
    }
}
