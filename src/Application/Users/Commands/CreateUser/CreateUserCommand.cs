using Application.Users.Common;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    /// <summary>
    /// Command to create a new user.
    /// </summary>
    public class CreateUserCommand: UserDto, IRequest<Guid>
    {
    }
}
