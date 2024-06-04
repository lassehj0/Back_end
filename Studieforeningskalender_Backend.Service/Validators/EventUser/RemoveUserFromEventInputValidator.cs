using FluentValidation;
using Studieforeningskalender_Backend.Domain.EventUsers;

namespace Studieforeningskalender_Backend.Service.Validators.EventUser;
public class RemoveUserFromEventInputValidator : AbstractValidator<RemoveUserFromEventInput>
{
	public RemoveUserFromEventInputValidator()
	{
		RuleFor(x => x.eventId).NotEmpty();
		RuleFor(x => x.username).NotEmpty().MaximumLength(64);
		RuleFor(x => x.isAdmin).NotEmpty();
	}
}
