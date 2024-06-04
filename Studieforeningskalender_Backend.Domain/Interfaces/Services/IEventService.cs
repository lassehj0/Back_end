using Studieforeningskalender_Backend.Domain.Events;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IEventService
	{
		IQueryable<EventDto> GetEvent();
		IQueryable<EventDto> GetEvents(string sorting, IList<string>? tags, string searchText);
		Task<CreateEventPayload> CreateEvent(CreateEventInput input);
		Task<DeleteEventPayload> DeleteEvent(Guid id);
	}
}
