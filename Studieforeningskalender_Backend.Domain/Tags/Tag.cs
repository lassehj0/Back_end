using HotChocolate.Types.Relay;
using Studieforeningskalender_Backend.Domain.EventTags;
using System.ComponentModel.DataAnnotations;

namespace Studieforeningskalender_Backend.Domain.Tags
{
	[Node]
	public class Tag
	{
		[ID]
		public Guid Id { get; set; }
		[MaxLength(100)]
		public string Name { get; set; }
		public IEnumerable<EventTag> EventTags { get; set; }
	}
}
