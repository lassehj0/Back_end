using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.UserRoles;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class UserRoleService : IUserRoleService
	{
		private readonly IUserRoleRepository _repository;

		public UserRoleService(IUserRoleRepository repository)
		{
			_repository = repository;
		}

		public IList<UserRole> GetRoleId(Guid id) =>
			_repository.GetRoleId(id);

		public async Task<UserRolesPayload> GrantRoleToUser(RoleAndUserInput input)
		{
			var userId = _repository.GetUserId(input.username);
			if (userId == null) return new UserRolesPayload(false, "The user was not found");

			var roleId = _repository.GetRoleId(input.roleName);
			if (roleId == null) return new UserRolesPayload(false, "The role was not found");

			var userRole = new UserRole()
			{
				UserId = (Guid)userId,
				RoleId = (Guid)roleId,
			};

			await _repository.GrantRoleToUser(userRole);

			return new UserRolesPayload(true, "Role granted to user successfully");
		}

		public async Task<UserRolesPayload> RemoveRoleFromUser(RoleAndUserInput input)
		{
			var userId = _repository.GetUserId(input.username);
			if (userId == null) return new UserRolesPayload(false, "The user was not found");

			var roleId = _repository.GetRoleId(input.roleName);
			if (roleId == null) return new UserRolesPayload(false, "The role was not found");

			var userRole = new UserRole()
			{
				UserId = (Guid)userId,
				RoleId = (Guid)roleId,
			};

			await _repository.RemoveRoleFromUser(userRole);

			return new UserRolesPayload(true, "Role removed from user successfully");
		}
	}
}