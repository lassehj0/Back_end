using Studieforeningskalender_Backend.Domain.Tags;
using Tag = Studieforeningskalender_Backend.Domain.Tags.Tag;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface ITagService
	{
		IQueryable<Tag> GetTags();
		Task<CreateTagPayload> CreateTag(CreateTagInput input);
		Task<CreateTagPayload> CreateTags(CreateTagsInput input);
		Task<DeleteTagPayload> DeleteTag(DeleteTagInput input);
	}
}
