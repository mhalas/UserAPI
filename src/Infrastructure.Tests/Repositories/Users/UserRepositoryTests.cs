using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Domain.Common;

namespace Infrastructure.Tests.Repositories.Users
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private DatabaseContext _context = null!;
        private UserRepository _repository = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DatabaseContext(options);
            _repository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task CreateAsync_ShouldAddUserToDatabase()
        {
            
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1)
            };

            
            var resultId = await _repository.CreateAsync(user, CancellationToken.None);

            
            var savedUser = await _context.AppUsers.FindAsync(resultId);
            Assert.That(savedUser, Is.Not.Null);
            Assert.That(savedUser!.FirstName, Is.EqualTo(user.FirstName));
        }

        [Test]
        public async Task GetAsync_ById_ShouldReturnUser()
        {
            
            var userId = Guid.NewGuid();
            var user = new AppUser { Id = userId, Username = "johndoe", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1) };
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            
            var result = await _repository.GetAsync(userId, CancellationToken.None);

            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
        }

        [Test]
        public async Task GetAsync_Paged_ShouldReturnPagedResults()
        {
            
            await _context.AppUsers.AddRangeAsync(new List<AppUser>
            {
                new AppUser { Id = Guid.NewGuid(), Username = "alice", FirstName = "Alice", LastName = "Z", BirthDate = new DateTime(1990, 1, 1) },
                new AppUser { Id = Guid.NewGuid(), Username = "bob", FirstName = "Bob", LastName = "Y", BirthDate = new DateTime(1990, 1, 1) },
                new AppUser { Id = Guid.NewGuid(), Username = "charlie", FirstName = "Charlie", LastName = "X", BirthDate = new DateTime(1990, 1, 1) }
            });
            await _context.SaveChangesAsync();

            var pagedRequest = new PagedRequest
            {
                PageNumber = 1,
                PageSize = 2,
                SortBy = "FirstName",
                IsDescending = false,
                FilterBy = new Dictionary<string, string>()
            };

            
            var result = await _repository.GetAsync(pagedRequest, CancellationToken.None);

            
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.Items[0].FirstName, Is.EqualTo("Alice"));
            Assert.That(result.Items[1].FirstName, Is.EqualTo("Bob"));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateExistingUser()
        {
            
            var userId = Guid.NewGuid();
            var user = new AppUser { Id = userId, Username = "johndoe", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1) };
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            var updatedUser = new AppUser { Id = userId, Username = "johnny", FirstName = "Johnny", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1) };

            
            var result = await _repository.UpdateAsync(updatedUser, CancellationToken.None);

            
            Assert.Multiple(() =>
            {
                Assert.That(result.FirstName, Is.EqualTo("Johnny"));
                Assert.That(result.Username, Is.EqualTo("johnny"));
            });
            
            var dbUser = await _context.AppUsers.FindAsync(userId);
            Assert.That(dbUser!.FirstName, Is.EqualTo("Johnny"));
        }

        [Test]
        public async Task RemoveAsync_ShouldDeleteUser()
        {
            
            var userId = Guid.NewGuid();
            var user = new AppUser { Id = userId, Username = "johndoe", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1) };
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            
            await _repository.RemoveAsync(userId, CancellationToken.None);

            
            var dbUser = await _context.AppUsers.FindAsync(userId);
            Assert.That(dbUser, Is.Null);
        }
    }
}
