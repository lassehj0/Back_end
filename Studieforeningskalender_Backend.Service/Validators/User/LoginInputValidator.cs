using FluentValidation;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Service.Validators.User;

public class LoginInputValidator : AbstractValidator<LoginInput>
{
	public LoginInputValidator()
	{
		RuleFor(x => x.UserName).NotEmpty().MaximumLength(64);
		RuleFor(x => x.Password).NotEmpty().Length(8, 255).Must(password => password.Any(char.IsUpper));
	}
}
