using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Common;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The user creation details.</param>
        /// <returns>The ID of the created user.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand request)
        {
            var result = await mediator.Send(request);
            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { Id = result },
                value: result);
        }

        /// <summary>
        /// Gets a paged list of users.
        /// </summary>
        /// <param name="query">The paging and filtering options.</param>
        /// <returns>A paged list of users.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersListQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="query">The user ID.</param>
        /// <returns>The user details.</returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetUserByIdQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user details.</param>
        /// <returns>No content.</returns>
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UserDto user)
        {
            var updateUserCommand = user.Adapt<UpdateUserCommand>();
            updateUserCommand.Id = id;

            await mediator.Send(updateUserCommand);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="request">The ID of the user to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
