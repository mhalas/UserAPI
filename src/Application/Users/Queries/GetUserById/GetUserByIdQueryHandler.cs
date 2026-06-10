using Application.Users.Common;
using Application.Users.Queries.GetUserById;
using Domain.Infrastructure.Repositories.Users;
using Mapster;
using MediatR;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(request.Id, cancellationToken);

            return user.Adapt<UserDto>();
        }
    }
}
