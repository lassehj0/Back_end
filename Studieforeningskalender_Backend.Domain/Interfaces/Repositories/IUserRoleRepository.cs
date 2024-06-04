using Studieforeningskalender_Backend.Domain.UserRoles;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IUserRoleRepository
	{
		IList<UserRole> GetRoleId(Guid id);
		Guid? GetUserId(string username);
		Guid? GetRoleId(string roleName);
		Task<int> GrantRoleToUser(UserRole userRole);
		Task<int> RemoveRoleFromUser(UserRole userRole);
	}
}