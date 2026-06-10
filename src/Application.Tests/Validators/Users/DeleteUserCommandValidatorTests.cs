using Application.Users.Commands.DeleteUser;
using Application.Validators.Users;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators.Users
{
    [TestFixture]
    public class DeleteUserCommandValidatorTests
    {
        private DeleteUserCommandValidator _validator = null!;

        [SetUp]
        public void SetUp()
        {
            _validator = new DeleteUserCommandValidator();
        }

        [Test]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteUserCommand { Id = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Test]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new DeleteUserCommand { Id = Guid.NewGuid() };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
