namespace Studieforeningskalender_Backend.Domain.Tags
{
	public record CreateTagPayload(bool isSuccessful, string message = "Created tag successfully");
	public record DeleteTagPayload(bool isSuccessful, string message = "Deleted tag successfully");
}
