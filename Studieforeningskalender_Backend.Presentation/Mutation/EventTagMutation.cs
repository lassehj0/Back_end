using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.EventTags;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class EventTagMutation
	{
		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<AttachTagToEventPayload> AttachTagToEvent([Service] IEventTagService eventTagService, EventAndTagInput input) =>
			await eventTagService.AttachTagToEvent(input);

		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<AttachTagsToEventPayload> AttachTagsToEvent([Service] IEventTagService eventTagService, EventAndTagsInput input) =>
			await eventTagService.AttachTagsToEvent(input);

		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<RemoveTagFromEventPayload> RemoveTagFromEvent([Service] IEventTagService eventTagService, EventAndTagInput input) =>
			await eventTagService.RemoveTagFromEvent(input);
	}
}
