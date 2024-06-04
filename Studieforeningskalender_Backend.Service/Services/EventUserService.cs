using Microsoft.AspNetCore.Http;
using Studieforeningskalender_Backend.Domain.EventUsers;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class EventUserService : IEventUserService
	{
		private readonly IEventUserRepository _repository;
		private readonly IUserRepository _userRepository;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IIdSerializer _serializer;

		public EventUserService(IEventUserRepository repository, IUserRepository userRepository, IHttpContextAccessor contextAccessor, IIdSerializer serializer)
		{
			_repository = repository;
			_userRepository = userRepository;
			_contextAccessor = contextAccessor;
			_serializer = serializer;
		}

		public async Task<AddUserToEventPayload> AddUserToEvent(AddUserToEventInput input)
		{
			var userId = _userRepository.GetUserByUsername(input.username)?.Id;

			if (userId == null)
				return new AddUserToEventPayload(false, "No user using that username exists");

			if (_repository.UserIsAttending(input.eventId, (Guid)userId))
				return new AddUserToEventPayload(true, "User is already registered for this event");

			var eventUser = new EventUser()
			{
				EventId = input.eventId,
				UserId = (Guid)userId,
				IsAdmin = input.isAdmin
			};

			await _repository.AddUserToEvent(eventUser);
			return new AddUserToEventPayload(true);
		}

		public async Task<AddUserToEventPayload> AddSelfToEvent(AddSelfToEventInput input)
		{
			var userClaims = _contextAccessor.HttpContext?.User.Claims;
			var username = userClaims?.FirstOrDefault(claim => claim.Type == "UserName")?.Value;
			if (username == null)
				return new AddUserToEventPayload(false, "Username could not be found in token");

			if (!Guid.TryParse(input.eventId, out Guid eventId))
			{
				IdValue deserializedId = _serializer.Deserialize(input.eventId);
				if (!Guid.TryParse(deserializedId.Value.ToString(), out eventId))
					return new AddUserToEventPayload(false, "There was an error with the event, go back and try again");
			}

			return await AddUserToEvent(new AddUserToEventInput(eventId, username, input.isAdmin));
		}

		public async Task<RemoveUserFromEventPayload> RemoveUserFromEvent(RemoveUserFromEventInput input)
		{
			var userId = _userRepository.GetUserByUsername(input.username)?.Id;

			if (userId == null)
				return new RemoveUserFromEventPayload(false, "No user by using that username exists");

			var eventUser = new EventUser()
			{
				EventId = input.eventId,
				UserId = (Guid)userId,
				IsAdmin = input.isAdmin
			};

			await _repository.RemoveUserFromEvent(eventUser);
			return new RemoveUserFromEventPayload(true);
		}
	}
}
