using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //creacion de la conexion a la base de datos, leyendo el connectionString ubicado en API
            services.AddDbContext<StreamerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("connectionString"))
            );
            //ingresando las inyeccioness
            //inyeccion de repositorybase
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            //inyeccion de repositories
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IStreamerRepository, StreamerRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailServices, EmailService>();


            return services;
        }
    }
}
