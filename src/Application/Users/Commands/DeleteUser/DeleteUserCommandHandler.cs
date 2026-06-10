using Domain.Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.RemoveAsync(request.Id, cancellationToken);
        }
    }
}
