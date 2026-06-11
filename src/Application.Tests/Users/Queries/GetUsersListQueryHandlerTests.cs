using Application.Users.Common;
using Application.Users.Queries.GetUsersList;
using Domain.Common;
using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;
using NSubstitute;

namespace Application.Tests.Users.Queries
{
    [TestFixture]
    public class GetUsersListQueryHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private GetUsersListQueryHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new GetUsersListQueryHandler(_userRepository);
        }

        [Test]
        public async Task Handle_ShouldMapFiltersToPagedRequest()
        {
            
            var query = new GetUsersListQuery
            {
                FilterUsername = "testuser",
                FilterFirstName = "John",
                FilterLastName = "Doe",
                FilterIsDeleted = true
            };
            var pagedResult = new PagedResult<AppUser>
            {
                Items = new List<AppUser>(),
                PageNumber = 1,
                PageSize = 10
            };

            _userRepository.GetAsync(Arg.Any<PagedRequest>(), Arg.Any<CancellationToken>())
                .Returns(pagedResult);

            
            await _handler.Handle(query, CancellationToken.None);

            
            await _userRepository.Received(1).GetAsync(Arg.Is<PagedRequest>(r => 
                r.FilterBy["Username"] == query.FilterUsername && 
                r.FilterBy["FirstName"] == query.FilterFirstName && 
                r.FilterBy["LastName"] == query.FilterLastName &&
                r.FilterBy["IsDeleted"] == "True"), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Handle_ShouldReturnPagedResultOfUserDto()
        {
            
            var query = new GetUsersListQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortBy = "FirstName",
                IsDescending = false
            };
            var pagedResult = new PagedResult<AppUser>
            {
                Items = new List<AppUser>
                {
                    new AppUser { Id = Guid.NewGuid(), Username = "johndoe", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990,1,1) }
                },
                PageNumber = 1,
                PageSize = 10
            };

            _userRepository.GetAsync(Arg.Any<PagedRequest>(), Arg.Any<CancellationToken>())
                .Returns(pagedResult);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count, Is.EqualTo(1));
                Assert.That(result.Items[0].Username, Is.EqualTo(pagedResult.Items[0].Username));
                Assert.That(result.PageNumber, Is.EqualTo(query.PageNumber));
                Assert.That(result.PageSize, Is.EqualTo(query.PageSize));
            });
            await _userRepository.Received(1).GetAsync(Arg.Is<PagedRequest>(r => 
                r.PageNumber == query.PageNumber && 
                r.PageSize == query.PageSize && 
                r.SortBy == query.SortBy && 
                r.IsDescending == query.IsDescending), Arg.Any<CancellationToken>());
        }
    }
}
