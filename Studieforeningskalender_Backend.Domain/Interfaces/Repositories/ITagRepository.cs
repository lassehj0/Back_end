using Tag = Studieforeningskalender_Backend.Domain.Tags.Tag;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface ITagRepository
	{
		Tag? GetTag(string tagName);
		Guid? GetTagId(string tagName);
		IList<Guid> GetTagIds(IList<string> tagName);
		IQueryable<Tag> GetTags();
		Task<int> CreateTag(Tag tag);
		Task<int> DeleteTag(Tag tag);
		Task<int> DeleteTag(Guid id);
	}
}
