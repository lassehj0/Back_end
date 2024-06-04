using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class EventMutation
	{
		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<CreateEventPayload> CreateEvent([Service] IEventService eventService, CreateEventInput input) =>
			await eventService.CreateEvent(input);

		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<DeleteEventPayload> DeleteEvent([Service] IEventService eventService, Guid id) =>
			await eventService.DeleteEvent(id);
	}
}
