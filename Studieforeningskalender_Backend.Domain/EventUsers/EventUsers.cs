using HotChocolate.Types.Relay;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Domain.EventUsers
{
	[Node]
	public class EventUser
	{
		[ID]
		public Guid EventId { get; set; }
		[ID]
		public Guid UserId { get; set; }
		public bool IsAdmin { get; set; }
		public Event Event { get; set; }
		public User User { get; set; }
	}
}
