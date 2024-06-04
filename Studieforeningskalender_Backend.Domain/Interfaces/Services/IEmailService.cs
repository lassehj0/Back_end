using Studieforeningskalender_Backend.Domain.Emails;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IEmailService
	{
		Task<VerificationEmailPayload> SendForgotPasswordVerificationEmail(string email, string reCaptchaToken);
		Task<VerificationEmailPayload> SendRegisterVerificationEmail(string email, Guid username, string firstName);
		Task<VerificationEmailPayload> ResendRegisterVerificationEmail();
	}
}
