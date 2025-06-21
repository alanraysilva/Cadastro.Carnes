using AutoMapper;
using System.Reflection;

namespace Cadastro.Carnes.Application.Mappings
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Ignora (opt.Ignore) toda propriedade de referência 
        /// (classes, coleções) do tipo de destino TD.
        /// Útil para pular navegações sem listá-las uma a uma.
        /// </summary>
        public static IMappingExpression<TS, TD> IgnoreNavigation<TS, TD>(
            this IMappingExpression<TS, TD> map)
        {
            var navs = typeof(TD)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                       !p.PropertyType.IsValueType &&            // não é struct / primitivo
                       p.PropertyType != typeof(string));        // nem string

            foreach (var p in navs)
                map.ForMember(p.Name, opt => opt.Ignore());

            return map;
        }
    }
}
