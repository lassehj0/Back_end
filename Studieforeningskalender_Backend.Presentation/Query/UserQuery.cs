using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Presentation.Query;

[ExtendObjectType(OperationTypeNames.Query)]
public class UserQuery
{
	[UseFirstOrDefault]
	[UseProjection]
	[UseFiltering]
	[Authorize]
	public IQueryable<User> GetUserInfo([Service] IUserService userService) =>
		userService.GetUserInfo();

	[UseProjection]
	[UseFiltering]
	[UseSorting]
	[Authorize(Roles = new string[] { "admin" })]
	public IQueryable<User> GetUsers([Service] IUserService userService) =>
		userService.GetUsers();

	[Authorize]
	public bool ValidateSession() => true;

	[Authorize(Roles = new[] { "studieforening", "admin" })]
	public bool IsAdminOrUnion() => true;
}


