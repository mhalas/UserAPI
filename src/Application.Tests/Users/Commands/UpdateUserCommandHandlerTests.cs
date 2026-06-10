using Application.Users.Commands.UpdateUser;
using Application.Users.Common;
using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;
using NSubstitute;

namespace Application.Tests.Users.Commands
{
    [TestFixture]
    public class UpdateUserCommandHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private UpdateUserCommandHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new UpdateUserCommandHandler(_userRepository);
        }

        [Test]
        public async Task Handle_ShouldReturnUpdatedUserDto()
        {
            
            var userId = Guid.NewGuid();
            var command = new UpdateUserCommand
            {
                Id = userId,
                Username = "janedoe",
                FirstName = "Jane",
                LastName = "Doe",
                BirthDate = new DateTime(1992, 2, 2)
            };
            var updatedUser = new User
            {
                Id = userId,
                Username = "janedoe",
                FirstName = "Jane",
                LastName = "Doe",
                BirthDate = new DateTime(1992, 2, 2)
            };
            _userRepository.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                .Returns(updatedUser);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            Assert.Multiple(() =>
            {
                Assert.That(result.Username, Is.EqualTo(command.Username));
                Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
                Assert.That(result.LastName, Is.EqualTo(command.LastName));
                Assert.That(result.BirthDate, Is.EqualTo(command.BirthDate));
            });
            await _userRepository.Received(1).UpdateAsync(Arg.Is<User>(u => 
                u.Id == userId &&
                u.Username == command.Username &&
                u.FirstName == command.FirstName && 
                u.LastName == command.LastName && 
                u.BirthDate == command.BirthDate), Arg.Any<CancellationToken>());
        }
    }
}
