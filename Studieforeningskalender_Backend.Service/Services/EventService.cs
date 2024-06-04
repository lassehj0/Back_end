using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.EventTags;
using Studieforeningskalender_Backend.Domain.EventUsers;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Tags;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class EventService : IEventService
	{
		private readonly IEventRepository _repository;
		private readonly ITagService _tagService;
		private readonly IEventTagService _eventTagService;
		private readonly IEventUserService _eventUserService;

		public EventService(IEventRepository repository, ITagService tagService, IEventTagService eventTagService, IEventUserService eventUserService)
		{
			_repository = repository;
			_tagService = tagService;
			_eventTagService = eventTagService;
			_eventUserService = eventUserService;
		}

		public IQueryable<EventDto> GetEvent() =>
			_repository.GetEvent();

		public IQueryable<EventDto> GetEvents(string sorting, IList<string>? tags, string searchText) =>
			_repository.GetEvents(sorting, tags, searchText);

		public async Task<CreateEventPayload> CreateEvent(CreateEventInput input)
		{
			byte[] bigImage, mediumImage, smallImage;
			using (var bigStream = new MemoryStream())
			{
				using var image = await Image.LoadAsync(input.image.OpenReadStream());

				await image.SaveAsJpegAsync(bigStream);
				bigImage = bigStream.ToArray();
			}

			using (var mediumStream = new MemoryStream())
			{
				using var image = await Image.LoadAsync(input.image.OpenReadStream());

				image.Mutate(x => x.Resize(640, 360));
				await image.SaveAsJpegAsync(mediumStream);
				mediumImage = mediumStream.ToArray();
			}

			using (var smallStream = new MemoryStream())
			{
				using var image = await Image.LoadAsync(input.image.OpenReadStream());

				image.Mutate(x => x.Resize(160, 90));
				await image.SaveAsJpegAsync(smallStream);
				smallImage = smallStream.ToArray();
			}

			var newEvent = new Event()
			{
				Id = Guid.NewGuid(),
				Title = input.title,
				Description = input.description,
				StartTime = input.startTime,
				EndTime = input.endTime,
				Image = bigImage,
				MediumImage = mediumImage,
				SmallImage = smallImage,
				Creation = DateTime.UtcNow,
				AddressLine = input.addressLine,
				City = input.city,
				PostalCode = input.postalCode,
			};

			await _repository.CreateEvent(newEvent);

			if (input.tags != null && input.tags.Count > 0)
			{
				await _tagService.CreateTags(new CreateTagsInput(input.tags));
				var tagsRes = await _eventTagService.AttachTagsToEvent(new EventAndTagsInput(newEvent.Id, input.tags));

				if (!tagsRes.isSuccessful)
					return new CreateEventPayload(false,
						$"An error occurred while attaching the tags to the event with the error message: {tagsRes.message}");
			}

			var res = await _eventUserService.AddSelfToEvent(new AddSelfToEventInput(newEvent.Id.ToString(), true));
			if (!res.isSuccessful)
				return new CreateEventPayload(false,
					$"An error occurred while adding the user to the event with the error message: {res.message}");

			return new CreateEventPayload(true);
		}

		public async Task<DeleteEventPayload> DeleteEvent(Guid id)
		{
			var delEvent = _repository.GetEventById(id);
			if (delEvent == null)
				return new DeleteEventPayload(false, "No event exists with that id");

			await _repository.DeleteEvent(delEvent);
			return new DeleteEventPayload(true);
		}
	}
}