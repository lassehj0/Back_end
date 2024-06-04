using FluentValidation;
using Studieforeningskalender_Backend.Domain.Tags;

namespace Studieforeningskalender_Backend.Service.Validators.Tag;
public class DeleteTagInputValidator : AbstractValidator<DeleteTagInput>
{
	public DeleteTagInputValidator()
	{
		RuleFor(x => x.tagName).NotEmpty().Length(1, 100);
	}
}
