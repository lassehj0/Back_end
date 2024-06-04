using FluentValidation;
using Studieforeningskalender_Backend.Domain.EventTags;

namespace Studieforeningskalender_Backend.Service.Validators.EventTag;

public class EventAndTagsInputValidator : AbstractValidator<EventAndTagsInput>
{
	public EventAndTagsInputValidator()
	{
		RuleFor(x => x.eventId).NotEmpty();
		RuleFor(x => x.tags).NotEmpty().ForEach(tag => tag.NotEmpty()).WithMessage("No tags can be null or empty");
	}
}
