namespace Studieforeningskalender_Backend.Domain.Tags
{
	public record GetTagsInput(IList<string> tagNames);
	public record CreateTagInput(string tagName);
	public record CreateTagsInput(IList<string> tagNames);
	public record DeleteTagInput(string tagName);
}
