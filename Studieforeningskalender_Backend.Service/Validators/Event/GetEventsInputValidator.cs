using FluentValidation;
using Studieforeningskalender_Backend.Domain.Events;

namespace Studieforeningskalender_Backend.Service.Validators.Event;
public class GetEventsInputValidator : AbstractValidator<GetEventsInput>
{
	public GetEventsInputValidator()
	{
		RuleFor(x => x.from).GreaterThanOrEqualTo(0);
		RuleFor(x => x.amount).GreaterThan(0);
		RuleFor(x => x.sorting.ToLower()).Must(x => x == "popular" || x == "soon");
	}
}
