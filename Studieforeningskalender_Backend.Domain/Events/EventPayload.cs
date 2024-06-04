namespace Studieforeningskalender_Backend.Domain.Events
{
	public record HomeBigEventPayload(string title, string description, IFile image);
	public record CreateEventPayload(bool isSuccessful, string message = "Created event successfully");
	public record DeleteEventPayload(bool isSuccessful, string message = "Deleted event successfully");
	public record ChatGPTPayload(bool isSuccessful, string response, string message = "ChatGPT responded successfully");
}
