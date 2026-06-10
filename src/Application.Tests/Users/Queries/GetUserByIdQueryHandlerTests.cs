using Application.Users.Common;
using Application.Users.Queries.GetSingleUser;
using Application.Users.Queries.GetUserById;
using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;
using NSubstitute;

namespace Application.Tests.Users.Queries
{
    [TestFixture]
    public class GetUserByIdQueryHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private GetUserByIdQueryHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new GetUserByIdQueryHandler(_userRepository);
        }

        [Test]
        public async Task Handle_ShouldReturnUserDto_WhenUserExists()
        {
            
            var userId = Guid.NewGuid();
            var query = new GetUserByIdQuery { Id = userId };
            var user = new User
            {
                Id = userId,
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1)
            };
            _userRepository.GetAsync(userId, Arg.Any<CancellationToken>())
                .Returns(user);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            Assert.Multiple(() =>
            {
                Assert.That(result.Username, Is.EqualTo(user.Username));
                Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
                Assert.That(result.LastName, Is.EqualTo(user.LastName));
                Assert.That(result.BirthDate, Is.EqualTo(user.BirthDate));
            });
            await _userRepository.Received(1).GetAsync(userId, Arg.Any<CancellationToken>());
        }
    }
}
