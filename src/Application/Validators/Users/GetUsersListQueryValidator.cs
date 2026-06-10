using Application.Users.Queries.GetUsersList;
using FluentValidation;

namespace Application.Validators.Users
{
    public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
    {
        public GetUsersListQueryValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }
}
