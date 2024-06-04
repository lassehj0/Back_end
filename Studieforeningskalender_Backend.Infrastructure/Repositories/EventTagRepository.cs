using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.EventTags;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class EventTagRepository : IEventTagRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<EventTag> _eventTag;

		public EventTagRepository(AppDbContext context)
		{
			_context = context;
			_eventTag = _context.EventTag;
		}

		public bool EventTagExists(EventTag eventTag) =>
			_eventTag.Any(et => et.TagId == eventTag.TagId && et.EventId == eventTag.EventId);

		public IList<Guid> GetEventIdsFromTag(Guid tagId) =>
			_eventTag.Where(x => x.TagId == tagId).Select(x => x.EventId).ToList();

		public IList<Guid> GetTagIdsFromEvent(Guid EventId) =>
			_eventTag.Where(x => x.EventId == EventId).Select(x => x.TagId).ToList();

		public async Task<int> AttachTagToEvent(EventTag eventTag)
		{
			_eventTag.Add(eventTag);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> AttachTagsToEvent(IList<EventTag> eventTags)
		{
			_eventTag.AddRange(eventTags);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> RemoveTagFromEvent(EventTag eventTag)
		{
			_eventTag.Remove(eventTag);
			return await _context.SaveChangesAsync();
		}
	}
}
