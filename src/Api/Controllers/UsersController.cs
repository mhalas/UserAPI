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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand request)
        {
            var result = await mediator.Send(request);
            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { Id = result },
                value: result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersListQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetUserByIdQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
