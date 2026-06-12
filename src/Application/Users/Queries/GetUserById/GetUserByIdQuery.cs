using Application.Users.Common;
using MediatR;

namespace Application.Users.Queries.GetUserById
{
    /// <summary>
    /// Query to get a user by their ID.
    /// </summary>
    public class GetUserByIdQuery: IRequest<UserDto>
    {
        /// <summary>
        /// The ID of the user to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
