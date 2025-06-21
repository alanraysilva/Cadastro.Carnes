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
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<ICarneRepository, CarneRepository>();
            services.AddScoped<ICarneService, CarneService>();

            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<ICidadeService, CidadeService>();

            services.AddScoped<ICompradorRepository, CompradorRepository>();
            services.AddScoped<ICompradorService, CompradorService>();

            services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
            services.AddScoped<IItemPedidoService, ItemPedidoService>();

            services.AddScoped<IMoedaRepository, MoedaRepository>();
            services.AddScoped<IMoedaService, MoedaService>();

            services.AddScoped<IOrigemRepository, OrigemRepository>();
            services.AddScoped<IOrigemService, OrigemService>();

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoService, PedidoService>();

            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
