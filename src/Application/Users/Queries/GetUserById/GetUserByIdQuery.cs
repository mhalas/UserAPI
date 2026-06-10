using Application.Users.Common;
using MediatR;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery: IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}
