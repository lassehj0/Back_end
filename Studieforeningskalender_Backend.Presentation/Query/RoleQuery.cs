using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Roles;

namespace Studieforeningskalender_Backend.Presentation.Query;

[ExtendObjectType(OperationTypeNames.Query)]

public class RoleQuery
{
	[Authorize(Roles = new string[] { "admin" })]
	[UseProjection]
	[UseFiltering]
	[UseSorting]
	public IQueryable<Role> GetRoles([Service] IRoleService roleService, IList<Guid>? roleIds = null) =>
		roleService.GetRoles(roleIds);
}

