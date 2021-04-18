using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Business_Logic_Layer.Services;

namespace Business_Logic_Layer
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services) 
        {
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(new MapperConfigurationClass()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<UserService>();
            services.AddTransient<LotService>();
            services.AddTransient<AuctionService>();
            return services;
        }
    }
}
