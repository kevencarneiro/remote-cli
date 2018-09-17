using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RemoteCLI.Application.AppServices;
using RemoteCLI.Application.Interfaces;
using RemoteCLI.Data.Repositories;
using RemoteCLI.Domain.DomainServices;
using RemoteCLI.Domain.Interfaces;

namespace RemoteCLI.IoC
{
    public static class Container
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Domain
            services.AddScoped<IClientDomainService, ClientDomainService>();

            // Data
            services.AddScoped<IClientRepository, ClientRepository>();

            // Application
            services.AddAutoMapper();
            services.AddScoped<IClientAppService, ClientAppService>();
        }
    }
}