using Application.Users.Commands.CreateUser;
using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;
using NSubstitute;

namespace Application.Tests.Users.Commands
{
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private CreateUserCommandHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new CreateUserCommandHandler(_userRepository);
        }

        [Test]
        public async Task Handle_ShouldReturnCreatedUserId()
        {
            
            var command = new CreateUserCommand
            {
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1)
            };
            var userId = Guid.NewGuid();
            _userRepository.CreateAsync(Arg.Any<AppUser>(), Arg.Any<CancellationToken>())
                .Returns(userId);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            Assert.That(result, Is.EqualTo(userId));
            await _userRepository.Received(1).CreateAsync(Arg.Is<AppUser>(u => 
                u.Username == command.Username &&
                u.FirstName == command.FirstName && 
                u.LastName == command.LastName && 
                u.BirthDate == command.BirthDate), Arg.Any<CancellationToken>());
        }
    }
}
