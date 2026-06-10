using Api.Controllers;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Common;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsersList;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private IMediator _mediator = null!;
        private UsersController _controller = null!;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new UsersController(_mediator);
        }

        [Test]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            
            var command = new CreateUserCommand();
            var userId = Guid.NewGuid();
            _mediator.Send(command).Returns(userId);

            
            var result = await _controller.Create(command);

            
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            var createdAtAction = (CreatedAtActionResult)result;
            Assert.That(createdAtAction.Value, Is.EqualTo(userId));
            Assert.That(createdAtAction.ActionName, Is.EqualTo(nameof(UsersController.Get)));
        }

        [Test]
        public async Task GetList_ShouldReturnOk()
        {
            
            var query = new GetUsersListQuery();
            var pagedResult = new PagedResult<UserDto>();
            _mediator.Send(query).Returns(pagedResult);

            
            var result = await _controller.Get(query);

            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(pagedResult));
        }

        [Test]
        public async Task GetById_ShouldReturnOk()
        {
            
            var query = new GetUserByIdQuery { Id = Guid.NewGuid() };
            var userDto = new UserDto();
            _mediator.Send(query).Returns(userDto);

            
            var result = await _controller.Get(query);

            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(userDto));
        }

        [Test]
        public async Task Update_ShouldReturnNoContent()
        {
            
            var userId = Guid.NewGuid();
            var userDto = new UserDto { FirstName = "Updated", Username = "updateduser" };

            
            var result = await _controller.Update(userId, userDto);

            
            Assert.That(result, Is.TypeOf<NoContentResult>());
            await _mediator.Received(1).Send(Arg.Is<UpdateUserCommand>(c => c.Id == userId && c.FirstName == userDto.FirstName));
        }

        [Test]
        public async Task Delete_ShouldReturnNoContent()
        {
            
            var command = new DeleteUserCommand { Id = Guid.NewGuid() };

            
            var result = await _controller.Delete(command);

            
            Assert.That(result, Is.TypeOf<NoContentResult>());
            await _mediator.Received(1).Send(command);
        }
    }
}
