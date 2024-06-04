using FluentValidation;
using Studieforeningskalender_Backend.Domain.EventTags;

namespace Studieforeningskalender_Backend.Service.Validators.EventTag;

public class EventAndTagInputValidator : AbstractValidator<EventAndTagInput>
{
	public EventAndTagInputValidator()
	{
		RuleFor(x => x.eventId).NotEmpty();
		RuleFor(x => x.tag).NotEmpty().Length(1, 100);
	}
}
