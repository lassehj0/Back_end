namespace Studieforeningskalender_Backend.Domain.EventTags
{
	public record EventAndTagInput(Guid eventId, string tag);
	public record EventAndTagsInput(Guid eventId, IList<string> tags);
}
