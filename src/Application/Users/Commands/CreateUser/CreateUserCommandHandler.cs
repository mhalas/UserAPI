using MediatR;
using Mapster;
using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = request.Adapt<User>();

            return await userRepository.CreateAsync(newUser, cancellationToken);
        }
    }
}
