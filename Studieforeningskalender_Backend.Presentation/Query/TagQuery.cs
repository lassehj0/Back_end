using HotChocolate;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Tag = Studieforeningskalender_Backend.Domain.Tags.Tag;

namespace Studieforeningskalender_Backend.Presentation.Query
{
	[ExtendObjectType(OperationTypeNames.Query)]
	public class TagQuery
	{
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Tag> GetTags([Service] ITagService tagService) =>
			tagService.GetTags();
	}
}
