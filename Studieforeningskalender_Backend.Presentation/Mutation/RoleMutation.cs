using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Roles;
using static Studieforeningskalender_Backend.Domain.Roles.RolePayload;

namespace Studieforeningskalender_backend.Presentation.Mutation
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class RoleMutation
	{
		[Authorize(Roles = new[] { "admin" })]
		public async Task<CreateRolePayload> CreateRole([Service] IRoleService roleService, RoleNameInput input) =>
			await roleService.CreateRole(input);

		[Authorize(Roles = new[] { "admin" })]
		public async Task<DeleteRolePayload> DeleteRole([Service] IRoleService roleService, RoleNameInput input) =>
			await roleService.DeleteRole(input);
	}
}
