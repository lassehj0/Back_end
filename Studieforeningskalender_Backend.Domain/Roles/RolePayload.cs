namespace Studieforeningskalender_Backend.Domain.Roles
{
	public class RolePayload
	{
		public record CreateRolePayload(bool isSuccessful, string message);
		public record DeleteRolePayload(bool isSuccessful, string message);
	}
}
