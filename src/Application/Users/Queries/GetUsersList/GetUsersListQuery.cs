using Application.Users.Common;
using Domain.Common;
using MediatR;

namespace Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery: IRequest<PagedResult<UserDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string SortBy { get; init; } = string.Empty;
        public bool IsDescending { get; set; } = false;
    }
}
