using MediatR;

namespace Application.Users.Commands.DeleteUser
{
    /// <summary>
    /// Command to delete a user.
    /// </summary>
    public class DeleteUserCommand: IRequest
    {
        /// <summary>
        /// The ID of the user to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
