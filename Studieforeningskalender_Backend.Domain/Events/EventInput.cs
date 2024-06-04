namespace Studieforeningskalender_Backend.Domain.Events
{
	public record GetEventsInput(int from, int amount, string sorting = "popular");
	public record CreateEventInput(string title, string description, DateTime startTime, DateTime endTime, IFile image, string addressLine, string city, string postalCode, IList<string>? tags, IList<string>? otherAdministrators);
}
