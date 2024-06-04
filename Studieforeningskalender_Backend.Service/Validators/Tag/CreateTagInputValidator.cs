using FluentValidation;
using Studieforeningskalender_Backend.Domain.Tags;

namespace Studieforeningskalender_Backend.Service.Validators.Tag;
public class CreateTagInputValidator : AbstractValidator<CreateTagInput>
{
	public CreateTagInputValidator()
	{
		RuleFor(x => x.tagName).NotEmpty().Length(1, 100);
	}
}
