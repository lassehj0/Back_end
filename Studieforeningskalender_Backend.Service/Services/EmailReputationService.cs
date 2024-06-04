using Microsoft.AspNetCore.Mvc;
using Studieforeningskalender_Backend.Domain.EmailReputations;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class EmailReputationService : IEmailReputationService
	{
		private readonly IEmailReputationRepository _repository;

		public EmailReputationService(IEmailReputationRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> IncreaseBounceCount(string emailAddress, bool isHardBounce)
		{
			var email = _repository.GetEmailReputation(emailAddress);

			if (email == null)
			{
				var emailReputation = new EmailReputation()
				{
					Id = Guid.NewGuid(),
					EmailAddress = emailAddress,
					HardBounces = isHardBounce ? 1 : 0,
					SoftBounces = isHardBounce ? 0 : 1
				};

				_repository.AddEmailReputation(emailReputation);
			}
			else
			{
				if (isHardBounce) email.HardBounces++;
				else email.SoftBounces++;
			}

			await _repository.SaveChangesAsync();
			return new OkResult();
		}

		public async Task<IActionResult> IncreaseComplaintCount(string emailAddress)
		{
			var email = _repository.GetEmailReputation(emailAddress);

			if (email == null)
			{
				var emailReputation = new EmailReputation()
				{
					Id = Guid.NewGuid(),
					EmailAddress = emailAddress,
					Complaints = 1,
				};

				_repository.AddEmailReputation(emailReputation);
			}
			else
			{
				email.Complaints++;
			}

			await _repository.SaveChangesAsync();
			return new OkResult();
		}

		public (bool hasBadReputation, string? message) CheckReputation(string emailAddress)
		{
			var email = _repository.GetEmailReputation(emailAddress);

			if (email == null) return (false, null);
			if (email.Complaints >= 1) return (true, "The email has had a complaint and can therefore not be used");
			if (email.HardBounces >= 1) return (true, "This email address has been marked as invalid or permanently unavailable, check for typos and try again");
			if (email.SoftBounces >= 6) return (true, "The email address has had too many soft rejections, this typically happens when the users inbox is full or the email servers are down");
			if (email.SoftBounces > 0) return (false, $"It seems that this email address has had {email.SoftBounces} soft rejection{(email.SoftBounces > 1 ? "s" : "")}, please make sure that your inbox is not full");

			return (false, null);
		}
	}
}
