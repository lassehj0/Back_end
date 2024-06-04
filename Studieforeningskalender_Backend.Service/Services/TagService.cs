using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Tags;
using Tag = Studieforeningskalender_Backend.Domain.Tags.Tag;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class TagService : ITagService
	{
		private readonly ITagRepository _repository;
		public TagService(ITagRepository repository)
		{
			_repository = repository;
		}

		public IQueryable<Tag> GetTags() =>
			_repository.GetTags();

		public async Task<CreateTagPayload> CreateTag(CreateTagInput input)
		{
			if (_repository.GetTag(input.tagName) == null)
			{
				var newTag = new Tag()
				{
					Id = Guid.NewGuid(),
					Name = input.tagName,
				};

				await _repository.CreateTag(newTag);
				return new CreateTagPayload(true);
			}

			return new CreateTagPayload(true, "Tag already exists");
		}

		public async Task<CreateTagPayload> CreateTags(CreateTagsInput input)
		{
			bool newTagCreated = false;
			foreach (var tagName in input.tagNames)
			{
				if (_repository.GetTag(tagName) == null)
				{
					var newTag = new Tag()
					{
						Id = Guid.NewGuid(),
						Name = tagName,
					};

					await _repository.CreateTag(newTag);
					newTagCreated = true;
				}
			}

			return new CreateTagPayload(true, newTagCreated ? "New tags successfully created" : "All tags already exists");
		}

		public async Task<DeleteTagPayload> DeleteTag(DeleteTagInput input)
		{
			var tag = _repository.GetTag(input.tagName);

			if (tag == null) return new DeleteTagPayload(false, "Tag does not exist");

			await _repository.DeleteTag(tag);
			return new DeleteTagPayload(true);
		}
	}
}
