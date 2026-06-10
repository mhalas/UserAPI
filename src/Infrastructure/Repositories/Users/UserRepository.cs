using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;
using Mapster;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository(DatabaseContext databaseContext) : BaseRepository<User>(databaseContext), IUserRepository
    {
        public override async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            var userToUpdate = await GetAsync(entity.Id, cancellationToken);
            entity.Adapt(userToUpdate);

            await databaseContext.SaveChangesAsync(cancellationToken);
            return userToUpdate;
        }
    }
}
