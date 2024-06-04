using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.UserRoles;

namespace Studieforeningskalender_backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class UserRoleMutation
	{
		[Authorize(Roles = new[] { "admin" })]
		public async Task<UserRolesPayload> GrantRoleToUser([Service] IUserRoleService userRoleService, RoleAndUserInput input) =>
			await userRoleService.GrantRoleToUser(input);

		[Authorize(Roles = new[] { "admin" })]
		public async Task<UserRolesPayload> RemoveRoleFromUser([Service] IUserRoleService userRoleService, RoleAndUserInput input) =>
			await userRoleService.RemoveRoleFromUser(input);
	}
}
