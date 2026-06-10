using Application.Users.Queries.GetUserById;
using Application.Validators.Users;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators.Users
{
    [TestFixture]
    public class GetUserByIdQueryValidatorTests
    {
        private GetUserByIdQueryValidator _validator = null!;

        [SetUp]
        public void SetUp()
        {
            _validator = new GetUserByIdQueryValidator();
        }

        [Test]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var query = new GetUserByIdQuery { Id = Guid.Empty };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Test]
        public void Should_Not_Have_Error_When_Query_Is_Valid()
        {
            var query = new GetUserByIdQuery { Id = Guid.NewGuid() };
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
