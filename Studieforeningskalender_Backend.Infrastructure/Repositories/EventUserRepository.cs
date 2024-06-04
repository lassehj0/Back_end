using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.EventUsers;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class EventUserRepository : IEventUserRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<EventUser> _eventUser;

		public EventUserRepository(AppDbContext context)
		{
			_context = context;
			_eventUser = _context.EventUser;
		}

		public async Task<int> AddUserToEvent(EventUser eventUser)
		{
			_eventUser.Add(eventUser);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> RemoveUserFromEvent(EventUser eventUser)
		{
			_eventUser.Remove(eventUser);
			return await _context.SaveChangesAsync();
		}

		public bool UserIsAttending(Guid eventId, Guid userId) =>
			_eventUser.Any(x => x.EventId == eventId && x.UserId == userId);
	}
}
