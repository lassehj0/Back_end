namespace Studieforeningskalender_Backend.Domain.EventUsers
{
	public record AddUserToEventPayload(bool isSuccessful, string message = "User successfully added to event");
	public record RemoveUserFromEventPayload(bool isSuccessful, string message = "User successfully removed from event");
}
