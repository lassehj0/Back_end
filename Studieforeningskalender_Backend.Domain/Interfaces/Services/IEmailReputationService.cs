using Microsoft.AspNetCore.Mvc;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IEmailReputationService
	{
		Task<IActionResult> IncreaseBounceCount(string emailAddress, bool isHardBounce);
		Task<IActionResult> IncreaseComplaintCount(string emailAddress);
		(bool hasBadReputation, string? message) CheckReputation(string emailAddress);
	}
}
