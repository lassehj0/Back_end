using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Roles;
using Studieforeningskalender_Backend.Domain.UserRoles;
using static Studieforeningskalender_Backend.Domain.Roles.RolePayload;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class RoleService : IRoleService
	{
		private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public IList<Role> GetRolesById(IList<UserRole> userRoles)
		{
			var roleIds = userRoles.Select(x => x.RoleId).ToList();
			return _roleRepository.GetRolesById(roleIds);
		}

		public IQueryable<Role> GetRoles(IList<Guid>? roleIds) =>
			_roleRepository.GetRoles(roleIds);

		public async Task<CreateRolePayload> CreateRole(RoleNameInput input)
		{
			var newRole = new Role()
			{
				Id = Guid.NewGuid(),
				Name = input.roleName,
			};

			await _roleRepository.CreateRole(newRole);

			return new CreateRolePayload(true, "Role was created successfully");
		}

		public async Task<DeleteRolePayload> DeleteRole(RoleNameInput input)
		{
			var role = _roleRepository.GetRoleByName(input.roleName);
			if (role == null) return new DeleteRolePayload(false, "Role to be deleted was not found");

			await _roleRepository.DeleteRole(role);

			return new DeleteRolePayload(true, "Role deleted successfully");
		}
	}
}
