using System.ComponentModel.DataAnnotations;

namespace Studieforeningskalender_Backend.Domain.EmailReputations
{
	[Node]
	public class EmailReputation
	{
		[ID]
		public Guid Id { get; set; }
		[EmailAddress]
		[MaxLength(100)]
		public string EmailAddress { get; set; }
		public int HardBounces { get; set; } = 0;
		public int SoftBounces { get; set; } = 0;
		public int Complaints { get; set; } = 0;
	}
}
