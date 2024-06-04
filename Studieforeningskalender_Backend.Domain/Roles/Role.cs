using HotChocolate.Types.Relay;
using Studieforeningskalender_Backend.Domain.UserRoles;
using System.ComponentModel.DataAnnotations;

namespace Studieforeningskalender_Backend.Domain.Roles
{
	[Node]
	public class Role
	{
		[ID]
		public Guid Id { get; set; }
		[MaxLength(15)]
		public string Name { get; set; }
		public IEnumerable<UserRole> UserRoles { get; set; }
	}
}
