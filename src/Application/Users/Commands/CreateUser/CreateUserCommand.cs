using Application.Users.Common;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand: UserDto, IRequest<Guid>
    {
    }
}
