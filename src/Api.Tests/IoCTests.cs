using Api;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsersList;
using Domain.Infrastructure.Repositories.Users;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests
{
    [TestFixture]
    public class IoCTests
    {
        private ServiceProvider _serviceProvider = null!;

        [SetUp]
        public void SetUp()
        {
            var builder = WebApplication.CreateBuilder();
            
            
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            
            
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("IoC_Test_Db");
            });

            IoC.RegisteredServices(builder.Services, builder.Configuration);
            
            _serviceProvider = builder.Services.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            _serviceProvider.Dispose();
        }

        [Test]
        [TestCase(typeof(DatabaseContext))]
        [TestCase(typeof(IUserRepository))]
        [TestCase(typeof(IMediator))]
        [TestCase(typeof(IValidator<CreateUserCommand>))]
        [TestCase(typeof(IValidator<UpdateUserCommand>))]
        [TestCase(typeof(IValidator<DeleteUserCommand>))]
        [TestCase(typeof(IValidator<GetUserByIdQuery>))]
        [TestCase(typeof(IValidator<GetUsersListQuery>))]
        public void Service_ShouldBeResolvable(Type serviceType)
        {
            
            var service = _serviceProvider.GetService(serviceType);

            
            Assert.That(service, Is.Not.Null, $"Failed to resolve {serviceType.Name}");
        }

        [Test]
        public void MediatR_Handlers_ShouldBeRegistered()
        {
            
            var mediator = _serviceProvider.GetService<IMediator>();
            Assert.That(mediator, Is.Not.Null);
        }
    }
}
