using Studieforeningskalender_Backend.Domain.EventUsers;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IEventUserRepository
	{
		Task<int> AddUserToEvent(EventUser eventUser);
		Task<int> RemoveUserFromEvent(EventUser eventUser);
		bool UserIsAttending(Guid eventId, Guid userId);
	}
}
