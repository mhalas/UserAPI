using Application.Users.Common;
using MediatR;
using Mapster;
using Domain.Entities;
using Domain.Infrastructure.Repositories.Users;


namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = request.Adapt<User>();

            var result = await userRepository.UpdateAsync(userToUpdate, cancellationToken);

            return result.Adapt<UserDto>();
        }
    }
}
