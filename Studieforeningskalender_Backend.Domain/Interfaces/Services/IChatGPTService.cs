using Studieforeningskalender_Backend.Domain.Events;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IChatGPTService
	{
		Task<ChatGPTPayload> GetChatGPTDescription(string prompt);
	}
}
