using Application.Users.Common;
using MediatR;

namespace Application.Users.Commands.UpdateUser
{
    /// <summary>
    /// Command to update an existing user.
    /// </summary>
    public class UpdateUserCommand: UserDto, IRequest<UserDto>
    {
        /// <summary>
        /// The ID of the user to update.
        /// </summary>
        public Guid Id { get; set; }
    }
}
