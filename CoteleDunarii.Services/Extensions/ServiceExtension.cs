using AutoMapper;
using CoteleDunarii.Services.Mappers.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoteleDunarii.Services.Extensions
{
    public static class ServiceExtension
    {
        public static IMapperConfigurationBuilder AddCoteleDunariiMapping(this IServiceCollection services)
        {
            var builder = new MapperConfigurationBuilder();

            services.AddSingleton<IConfigurationProvider>(sp => new MapperConfiguration(cfg =>
            {
                foreach (var profileType in builder.ProfileTypes)
                    cfg.AddProfile(profileType);
            }));

            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            return builder;
        }
    }
}
