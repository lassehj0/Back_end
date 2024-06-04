using Studieforeningskalender_Backend.Domain.EventTags;
using Studieforeningskalender_Backend.Domain.EventUsers;
using System.ComponentModel.DataAnnotations;

namespace Studieforeningskalender_Backend.Domain.Events
{
	[Node]
	public class EventDto
	{
		[ID]
		public Guid Id { get; set; }
		[MaxLength(64)]
		public string Title { get; set; }
		[MaxLength(5000)]
		public string Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Image { get; set; }
		public string MediumImage { get; set; }
		public string SmallImage { get; set; }
		public DateTime Creation { get; set; }
		[MaxLength(200)]
		public string AddressLine { get; set; }
		[MaxLength(50)]
		public string City { get; set; }
		[MaxLength(4)]
		public string PostalCode { get; set; }
		public IEnumerable<EventTag> EventTags { get; set; }
		public IEnumerable<EventUser> EventUsers { get; set; }
	}
}
