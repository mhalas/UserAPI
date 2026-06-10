using Application.Users.Queries.GetUsersList;
using Application.Validators.Users;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators.Users
{
    [TestFixture]
    public class GetUsersListQueryValidatorTests
    {
        private GetUsersListQueryValidator _validator = null!;

        [SetUp]
        public void SetUp()
        {
            _validator = new GetUsersListQueryValidator();
        }

        [Test]
        public void Should_Have_Error_When_PageNumber_Is_Empty()
        {
            
            var query = new GetUsersListQuery { PageNumber = 0 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Test]
        public void Should_Have_Error_When_PageSize_Is_Empty()
        {
            var query = new GetUsersListQuery { PageSize = 0 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Test]
        public void Should_Not_Have_Error_When_Query_Is_Valid()
        {
            var query = new GetUsersListQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
