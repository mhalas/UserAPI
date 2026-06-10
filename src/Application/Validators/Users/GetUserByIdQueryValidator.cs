using Application.Users.Queries.GetUserById;
using FluentValidation;

namespace Application.Validators.Users
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
