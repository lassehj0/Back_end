using Studieforeningskalender_Backend.Domain.EventTags;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IEventTagRepository
	{
		bool EventTagExists(EventTag eventTag);
		IList<Guid> GetEventIdsFromTag(Guid tagId);
		IList<Guid> GetTagIdsFromEvent(Guid eventId);
		Task<int> AttachTagToEvent(EventTag eventTag);
		Task<int> AttachTagsToEvent(IList<EventTag> eventTag);
		Task<int> RemoveTagFromEvent(EventTag eventTag);
	}
}
