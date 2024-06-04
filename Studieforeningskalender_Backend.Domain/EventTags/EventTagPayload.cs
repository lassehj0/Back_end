namespace Studieforeningskalender_Backend.Domain.EventTags
{
	public record AttachTagToEventPayload(bool isSuccessful, string message = "Tag successfully attached to event");
	public record AttachTagsToEventPayload(bool isSuccessful, string message = "Tags successfully attached to event");
	public record RemoveTagFromEventPayload(bool isSuccessful, string message = "Tag successfully removed from event");
}
