using FluentValidation;
using Studieforeningskalender_Backend.Domain.UserRoles;

namespace Studieforeningskalender_Backend.Service.Validators.UserRole;

public class RoleAndUserInputValidator : AbstractValidator<RoleAndUserInput>
{
	public RoleAndUserInputValidator()
	{
		RuleFor(x => x.username).NotEmpty().MaximumLength(64);
		RuleFor(x => x.roleName).NotEmpty().MaximumLength(15);
	}
}
