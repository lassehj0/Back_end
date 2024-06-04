using FluentValidation;
using Studieforeningskalender_Backend.Domain.Roles;

namespace Studieforeningskalender_Backend.Service.Validators.Role;

public class RoleInputValidator : AbstractValidator<RoleNameInput>
{
	public RoleInputValidator()
	{
		RuleFor(x => x.roleName).NotEmpty().MaximumLength(15);
	}
}
