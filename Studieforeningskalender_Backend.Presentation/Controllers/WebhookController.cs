using Microsoft.AspNetCore.Mvc;
using Studieforeningskalender_Backend.Domain.EmailReputations;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_Backend.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WebhookController
	{
		private readonly IEmailReputationService _service;

		public WebhookController(IEmailReputationService service)
		{
			_service = service;
		}

		[HttpPost("increaseComplaintCount")]
		public async Task<IActionResult> IncreaseComplaintCount([FromBody] BrevoObject data) =>
			await _service.IncreaseComplaintCount(data.Email);

		[HttpPost("increaseHardBounceCount")]
		public async Task<IActionResult> IncreaseHardBounceCount([FromBody] BrevoObject data) =>
			await _service.IncreaseBounceCount(data.Email, true);

		[HttpPost("increaseSoftBounceCount")]
		public async Task<IActionResult> IncreaseSoftBounceCount([FromBody] BrevoObject data) =>
			await _service.IncreaseBounceCount(data.Email, false);
	}
}
