using FluentValidation;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Service.Validators.User;

public class UpdateUserInputValidator : AbstractValidator<UpdateUserInput>
{
	public UpdateUserInputValidator()
	{
		RuleFor(x => x.FirstName).MaximumLength(50).Unless(x => string.IsNullOrEmpty(x.FirstName));
		RuleFor(x => x.LastName).MaximumLength(50).Unless(x => string.IsNullOrEmpty(x.LastName));
		RuleFor(x => x.UserName).MaximumLength(64).Unless(x => string.IsNullOrEmpty(x.UserName));
	}
}
