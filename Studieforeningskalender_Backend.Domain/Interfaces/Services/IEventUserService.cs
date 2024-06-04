using Studieforeningskalender_Backend.Domain.EventUsers;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IEventUserService
	{
		Task<AddUserToEventPayload> AddUserToEvent(AddUserToEventInput input);
		Task<AddUserToEventPayload> AddSelfToEvent(AddSelfToEventInput input);
		Task<RemoveUserFromEventPayload> RemoveUserFromEvent(RemoveUserFromEventInput input);
	}
}
