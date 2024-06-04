using Studieforeningskalender_Backend.Domain.Roles;
using Studieforeningskalender_Backend.Domain.UserRoles;
using static Studieforeningskalender_Backend.Domain.Roles.RolePayload;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IRoleService
	{
		IList<Role> GetRolesById(IList<UserRole> userRoles);
		IQueryable<Role> GetRoles(IList<Guid>? roleIds);
		Task<CreateRolePayload> CreateRole(RoleNameInput input);
		Task<DeleteRolePayload> DeleteRole(RoleNameInput input);
	}
}
