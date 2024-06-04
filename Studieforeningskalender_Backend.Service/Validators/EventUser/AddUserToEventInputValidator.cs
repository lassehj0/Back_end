using FluentValidation;
using Studieforeningskalender_Backend.Domain.EventUsers;

namespace Studieforeningskalender_Backend.Service.Validators.EventUser;
public class AddUserToEventInputValidator : AbstractValidator<AddUserToEventInput>
{
	public AddUserToEventInputValidator()
	{
		RuleFor(x => x.eventId).NotEmpty();
		RuleFor(x => x.username).NotEmpty().MaximumLength(64);
	}
}
