using Application.Users.Commands.UpdateUser;
using Application.Validators.Users;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators.Users
{
    [TestFixture]
    public class UpdateUserCommandValidatorTests
    {
        private UpdateUserCommandValidator _validator = null!;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateUserCommandValidator();
        }

        [Test]
        public void Should_Have_Error_When_Required_Fields_Are_Empty()
        {
            var command = new UpdateUserCommand { Id = Guid.Empty, Username = "", FirstName = "", LastName = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.Username);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Test]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                Username = "janedoe",
                FirstName = "Jane",
                LastName = "Doe",
                BirthDate = new DateTime(1992, 2, 2)
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
