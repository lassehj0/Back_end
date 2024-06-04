using Studieforeningskalender_Backend.Domain.EventTags;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class EventTagService : IEventTagService
	{
		private readonly IEventTagRepository _repository;
		private readonly ITagRepository _tagRepository;

		public EventTagService(IEventTagRepository repository, ITagRepository tagRepository)
		{
			_repository = repository;
			_tagRepository = tagRepository;
		}

		public async Task<AttachTagToEventPayload> AttachTagToEvent(EventAndTagInput input)
		{
			var tagId = _tagRepository.GetTagId(input.tag);
			if (tagId == null)
				return new AttachTagToEventPayload(false, "The specified tag was not found");

			var newEventTag = new EventTag()
			{
				EventId = input.eventId,
				TagId = (Guid)tagId,
			};

			await _repository.AttachTagToEvent(newEventTag);
			return new AttachTagToEventPayload(true);
		}

		public async Task<AttachTagsToEventPayload> AttachTagsToEvent(EventAndTagsInput input)
		{
			var tagIds = _tagRepository.GetTagIds(input.tags);

			if (tagIds == null || tagIds.Count == 0)
				return new AttachTagsToEventPayload(false, "The specified tags do not exist");

			IList<EventTag> eventTags = new List<EventTag>();
			foreach (var tagId in tagIds)
			{
				var eventTag = new EventTag()
				{
					TagId = tagId,
					EventId = input.eventId,
				};

				if (!_repository.EventTagExists(eventTag))
					eventTags.Add(eventTag);
			}

			await _repository.AttachTagsToEvent(eventTags);
			return new AttachTagsToEventPayload(true);
		}

		public async Task<RemoveTagFromEventPayload> RemoveTagFromEvent(EventAndTagInput input)
		{
			var tagId = _tagRepository.GetTagId(input.tag);
			if (tagId == null)
				return new RemoveTagFromEventPayload(false, "The specified tag was not found");

			var removeEventTag = new EventTag()
			{
				EventId = input.eventId,
				TagId = (Guid)tagId,
			};

			await _repository.RemoveTagFromEvent(removeEventTag);

			var eventTagIds = _repository.GetEventIdsFromTag((Guid)tagId);
			if (eventTagIds == null || eventTagIds.Count == 0)
				await _tagRepository.DeleteTag((Guid)tagId);

			return new RemoveTagFromEventPayload(true);
		}
	}
}
