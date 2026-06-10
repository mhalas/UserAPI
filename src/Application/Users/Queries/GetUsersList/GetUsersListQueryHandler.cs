using Application.Users.Common;
using Domain.Common;
using Domain.Infrastructure.Repositories.Users;
using Mapster;
using MediatR;

namespace Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersListQuery, PagedResult<UserDto>>
    {
        public async Task<PagedResult<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var pagedRequest = request.Adapt<PagedRequest>();

            var result = await userRepository.GetAsync(pagedRequest, cancellationToken);
            return result.Adapt<PagedResult<UserDto>>();
        }
    }
}
