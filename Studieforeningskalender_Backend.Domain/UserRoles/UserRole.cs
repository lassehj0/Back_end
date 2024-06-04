using HotChocolate.Types.Relay;
using Studieforeningskalender_Backend.Domain.Roles;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Domain.UserRoles
{
	[Node]
	public class UserRole
	{
		[ID]
		public Guid UserId { get; set; }
		[ID]
		public Guid RoleId { get; set; }

		public User User { get; set; }
		public Role Role { get; set; }
	}
}
