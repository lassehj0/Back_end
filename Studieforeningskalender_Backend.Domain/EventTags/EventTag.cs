using Studieforeningskalender_Backend.Domain.Events;
using Tag = Studieforeningskalender_Backend.Domain.Tags.Tag;

namespace Studieforeningskalender_Backend.Domain.EventTags
{
	[Node]
	public class EventTag
	{
		[ID]
		public Guid EventId { get; set; }
		[ID]
		public Guid TagId { get; set; }

		public Event Event { get; set; }
		public Tag Tag { get; set; }
	}
}
