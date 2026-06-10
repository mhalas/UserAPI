using Api.Exceptions;
using Application.Behaviors;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsersList;
using Application.Validators.Users;
using Domain.Infrastructure.Repositories.Users;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;

namespace Api
{
    public class IoC
    {
        public static void RegisteredServices(IServiceCollection services, ConfigurationManager configurationManager)
        {
            RegisterDbContext(services, configurationManager);

            RegisterRepositories(services);

            RegisterSwagger(services);

            RegisterValidators(services);

            RegisterMediatR(services);

            RegisterExceptionHandlers(services);

            services.AddControllers();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void RegisterExceptionHandlers(IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddProblemDetails();
        }

        private static void RegisterMediatR(IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<GetUsersListQueryHandler>();
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
            services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserCommandValidator>();
            services.AddScoped<IValidator<GetUserByIdQuery>, GetUserByIdQueryValidator>();
            services.AddScoped<IValidator<GetUsersListQuery>, GetUsersListQueryValidator>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User API",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Maciej Hałas",
                        Email = "maciej.halas@outlook.com"
                    }
                });

                
                
            });
        }

        private static void RegisterDbContext(IServiceCollection services, ConfigurationManager configurationManager)
        {
            
            if (string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Testing", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var connectionString = configurationManager.GetConnectionString("DatabaseConnection");
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
