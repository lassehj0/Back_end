using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_Backend.Presentation.Query;

[ExtendObjectType(OperationTypeNames.Query)]
public class EventQuery
{
	[UseFirstOrDefault]
	[UseProjection]
	[UseFiltering]
	public IQueryable<EventDto> GetEvent([Service] IEventService eventService) =>
		eventService.GetEvent();

	[UseOffsetPaging(IncludeTotalCount = true)]
	[UseProjection]
	[UseFiltering]
	[UseSorting]
	public IQueryable<EventDto> GetEvents([Service] IEventService eventService, string sorting = "", IList<string>? tags = null, string searchText = "") =>
		eventService.GetEvents(sorting, tags, searchText);

	[Authorize(Roles = new[] { "studieforening", "admin" })]
	public Task<ChatGPTPayload> GetChatGPTDescription([Service] IChatGPTService service, string prompt) =>
		service.GetChatGPTDescription(prompt);
}

