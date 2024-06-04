using Studieforeningskalender_Backend.Domain.EventUsers;
using Studieforeningskalender_Backend.Domain.UserRoles;
using System.ComponentModel.DataAnnotations;

namespace Studieforeningskalender_Backend.Domain.Users
{
	[Node]
	public class User
	{
		[ID]
		public Guid Id { get; set; }
		[MaxLength(64)]
		public string UserName { get; set; }
		[MinLength(8)]
		[MaxLength(255)]
		public string Password { get; set; }
		[MaxLength(50)]
		public string FirstName { get; set; }
		[MaxLength(50)]
		public string LastName { get; set; }
		[EmailAddress]
		[MaxLength(100)]
		public string EmailAddress { get; set; }
		public IEnumerable<UserRole> UserRoles { get; set; }
		public IEnumerable<EventUser> EventUsers { get; set; }
	}
}
