using Application.Users.Commands.DeleteUser;
using Domain.Infrastructure.Repositories.Users;
using NSubstitute;

namespace Application.Tests.Users.Commands
{
    [TestFixture]
    public class DeleteUserCommandHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private DeleteUserCommandHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new DeleteUserCommandHandler(_userRepository);
        }

        [Test]
        public async Task Handle_ShouldCallRemoveAsync()
        {
            
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand { Id = userId };

            
            await _handler.Handle(command, CancellationToken.None);

            
            await _userRepository.Received(1).RemoveAsync(userId, Arg.Any<CancellationToken>());
        }
    }
}
