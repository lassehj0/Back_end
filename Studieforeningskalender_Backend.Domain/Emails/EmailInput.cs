namespace Studieforeningskalender_Backend.Domain.Emails
{
	public record ForgotPasswordVerificationEmailInput(string email, string reCaptchaToken);
	public record RegisterVerificationEmailInput(string email, Guid userId, string firstName);
}
