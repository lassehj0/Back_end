using Studieforeningskalender_Backend.Domain.Events;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IEventRepository
	{
		IQueryable<EventDto> GetEvent();
		Event? GetEventById(Guid id);
		IQueryable<EventDto> GetEvents(string sorting, IList<string>? tags, string searchText);
		Task<int> CreateEvent(Event payload);
		Task<int> DeleteEvent(Event delEvent);
	}
}
