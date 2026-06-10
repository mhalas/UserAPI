using Application.Users.Commands.CreateUser;
using Application.Validators.Users;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators.Users
{
    [TestFixture]
    public class CreateUserCommandValidatorTests
    {
        private CreateUserCommandValidator _validator = null!;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateUserCommandValidator();
        }

        [Test]
        public void Should_Have_Error_When_Username_Is_Empty()
        {
            var command = new CreateUserCommand { Username = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Test]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            var command = new CreateUserCommand { FirstName = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            var command = new CreateUserCommand { LastName = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Test]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateUserCommand
            {
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1)
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
