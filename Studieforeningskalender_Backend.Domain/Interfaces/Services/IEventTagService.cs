using Studieforeningskalender_Backend.Domain.EventTags;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IEventTagService
	{
		Task<AttachTagToEventPayload> AttachTagToEvent(EventAndTagInput input);
		Task<AttachTagsToEventPayload> AttachTagsToEvent(EventAndTagsInput input);
		Task<RemoveTagFromEventPayload> RemoveTagFromEvent(EventAndTagInput input);
	}
}
