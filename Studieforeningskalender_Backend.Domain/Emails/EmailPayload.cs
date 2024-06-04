namespace Studieforeningskalender_Backend.Domain.Emails
{
	public record VerificationEmailPayload(bool isSuccessful, string message = "The verification email was successfully sent");
}
