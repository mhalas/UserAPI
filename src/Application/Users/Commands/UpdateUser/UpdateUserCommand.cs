using Application.Users.Common;
using MediatR;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand: UserDto, IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}
