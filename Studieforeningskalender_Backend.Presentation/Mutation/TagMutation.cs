using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Tags;

namespace Studieforeningskalender_Backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class TagMutation
	{
		[Authorize(Roles = new[] { "admin" })]
		public async Task<CreateTagPayload> CreateTag([Service] ITagService tagService, CreateTagInput input) =>
			await tagService.CreateTag(input);


		[Authorize(Roles = new[] { "admin" })]
		public async Task<CreateTagPayload> CreateTags([Service] ITagService tagService, CreateTagsInput input) =>
			await tagService.CreateTags(input);

		[Authorize(Roles = new[] { "admin" })]
		public async Task<DeleteTagPayload> DeleteTag([Service] ITagService tagService, DeleteTagInput input) =>
			await tagService.DeleteTag(input);
	}
}
