using FluentValidation;
using Studieforeningskalender_Backend.Domain.Tags;

namespace Studieforeningskalender_Backend.Service.Validators.Tag;
public class CreateTagsInputValidator : AbstractValidator<CreateTagsInput>
{
	public CreateTagsInputValidator()
	{
		RuleFor(x => x.tagNames).ForEach(tagName => tagName.NotEmpty().Length(1, 100));
	}
}
