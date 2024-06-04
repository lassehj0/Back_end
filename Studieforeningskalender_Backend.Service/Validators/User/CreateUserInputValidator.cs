using FluentValidation;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Service.Validators.User;

public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
{
	public CreateUserInputValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
		RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
		RuleFor(x => x.UserName).NotEmpty().MaximumLength(64);
		RuleFor(x => x.Password).NotEmpty().Length(8, 255).Must(password => password.Any(char.IsUpper));

		RuleFor(x => x.EmailAddress).NotEmpty().Length(11, 100).EmailAddress();
		RuleFor(x => x.EmailAddress).Must(email => email.Contains("@post.au.dk"))
			.WithMessage("Email address must be a @post.au.dk email");
	}
}
