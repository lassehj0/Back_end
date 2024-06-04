using Studieforeningskalender_Backend.Domain.UserRoles;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IUserRoleService
	{
		IList<UserRole> GetRoleId(Guid id);
		Task<UserRolesPayload> GrantRoleToUser(RoleAndUserInput input);
		Task<UserRolesPayload> RemoveRoleFromUser(RoleAndUserInput input);
	}
}