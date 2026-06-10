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

            pagedRequest.FilterBy = request.GetType()
                .GetProperties()
                .Where(p => p.Name.StartsWith("Filter"))
                .Select(p => new { Key = p.Name.Replace("Filter", ""), Value = p.GetValue(request) })
                .Where(x => x.Value != null && !string.IsNullOrWhiteSpace(x.Value.ToString()))
                .ToDictionary(x => x.Key, x => x.Value!.ToString()!);

            var result = await userRepository.GetAsync(pagedRequest, cancellationToken);
            return result.Adapt<PagedResult<UserDto>>();
        }
    }
}
