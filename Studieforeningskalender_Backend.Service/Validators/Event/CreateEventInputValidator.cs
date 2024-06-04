using FluentValidation;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Service.Helpers;
using static Studieforeningskalender_Backend.Service.Helpers.ValidationHelper;

namespace Studieforeningskalender_Backend.Service.Validators.Event;
public class CreateEventInputValidator : AbstractValidator<CreateEventInput>
{
	public CreateEventInputValidator()
	{
		RuleFor(x => x.title).NotEmpty().Length(2, 64);
		RuleFor(x => x.description).NotEmpty().MaximumLength(5000);

		RuleFor(x => x.startTime).NotEmpty()
			.Must(startTime => startTime > DateTime.Now)
			.WithMessage("The start time must be later than the current time");

		RuleFor(x => x).NotEmpty()
			.Must(x => x.endTime < x.startTime.AddDays(14))
			.WithMessage("Event cannot be more than two weeks long")
			.Must(x => x.endTime > x.startTime)
			.WithMessage("Event cannot end before the start time");

		RuleFor(x => x.image).NotEmpty().MustHaveDimensions().MustHaveRatio();

		RuleFor(x => x.tags)
			.Must(tags => tags.Count() <= 10)
			.WithMessage("You cannot have more than 10 tags for one event")
			.ForEach(tag => tag.NotEmpty())
			.WithMessage("No tags can be null or empty")
			.Unless(x => x.tags == null || x.tags.Count == 0);

		RuleFor(x => x.otherAdministrators)
			.ForEach(otherAdmin => otherAdmin.NotEmpty())
			.WithMessage("No added admin names can be null or empty")
			.Unless(x => x.otherAdministrators == null || x.otherAdministrators.Count == 0);
	}
}