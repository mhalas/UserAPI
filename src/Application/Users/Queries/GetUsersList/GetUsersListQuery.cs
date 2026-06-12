using Application.Users.Common;
using Domain.Common;
using MediatR;

namespace Application.Users.Queries.GetUsersList
{
    /// <summary>
    /// Query to get a paged list of users with filtering and sorting.
    /// </summary>
    public class GetUsersListQuery: IRequest<PagedResult<UserDto>>
    {
        /// <summary>
        /// The page number to retrieve. Defaults to 1.
        /// </summary>
        public int PageNumber { get; init; } = 1;

        /// <summary>
        /// The number of items per page. Defaults to 10.
        /// </summary>
        public int PageSize { get; init; } = 10;

        /// <summary>
        /// The property name to sort by.
        /// </summary>
        public string SortBy { get; init; } = string.Empty;

        /// <summary>
        /// Indicates if the sorting should be in descending order.
        /// </summary>
        public bool IsDescending { get; set; } = false;

        /// <summary>
        /// Filter by username.
        /// </summary>
        public string? FilterUsername { get; set; }

        /// <summary>
        /// Filter by first name.
        /// </summary>
        public string? FilterFirstName { get; set; }

        /// <summary>
        /// Filter by last name.
        /// </summary>
        public string? FilterLastName { get; set; }

        /// <summary>
        /// Filter by soft-deleted status.
        /// </summary>
        public bool? FilterIsDeleted { get; set; } = false;
    }
}
