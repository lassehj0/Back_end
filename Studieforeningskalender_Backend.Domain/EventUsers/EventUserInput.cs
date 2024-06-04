namespace Studieforeningskalender_Backend.Domain.EventUsers
{
	public record AddUserToEventInput(Guid eventId, string username, bool isAdmin);
	public record AddSelfToEventInput([ID] string eventId, bool isAdmin = false);
	public record RemoveUserFromEventInput(Guid eventId, string username, bool isAdmin);
}
