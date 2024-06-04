using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.EventUsers;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class EventUserMutation
	{
		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<AddUserToEventPayload> AddUserToEvent([Service] IEventUserService eventUserService, AddUserToEventInput input) =>
			await eventUserService.AddUserToEvent(input);

		[Authorize(Policy = "verified")]
		public async Task<AddUserToEventPayload> AddSelfToEvent([Service] IEventUserService eventUserService, AddSelfToEventInput input) =>
			await eventUserService.AddSelfToEvent(input);

		[Authorize(Roles = new[] { "studieforening", "admin" })]
		public async Task<RemoveUserFromEventPayload> RemoveUserFromEvent([Service] IEventUserService eventUserService, RemoveUserFromEventInput input) =>
			await eventUserService.RemoveUserFromEvent(input);
	}
}
