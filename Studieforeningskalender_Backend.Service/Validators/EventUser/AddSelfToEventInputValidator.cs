using FluentValidation;
using Studieforeningskalender_Backend.Domain.EventUsers;

namespace Studieforeningskalender_Backend.Service.Validators.EventUser;
public class AddSelfToEventInputValidator : AbstractValidator<AddSelfToEventInput>
{
	public AddSelfToEventInputValidator()
	{
		RuleFor(x => x.eventId).NotEmpty();
	}
}
