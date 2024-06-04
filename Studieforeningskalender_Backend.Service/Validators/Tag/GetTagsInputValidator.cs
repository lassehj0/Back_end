using FluentValidation;
using Studieforeningskalender_Backend.Domain.Tags;

namespace Studieforeningskalender_Backend.Service.Validators.Tag;
public class GetTagsInputValidator : AbstractValidator<GetTagsInput>
{
	public GetTagsInputValidator()
	{
		RuleFor(x => x.tagNames).ForEach(tagName => tagName.NotEmpty().Length(1, 100));
	}
}
