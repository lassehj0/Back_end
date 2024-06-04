using Studieforeningskalender_Backend.Domain.Roles;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IRoleRepository
	{
		IList<Role> GetRolesById(IList<Guid> roleIds);
		IQueryable<Role> GetRoles(IList<Guid>? roleIds);
		Task<int> CreateRole(Role role);
		Role GetRoleByName(string roleName);
		Task<int> DeleteRole(Role role);
	}
}
