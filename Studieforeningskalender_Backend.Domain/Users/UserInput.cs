namespace Studieforeningskalender_Backend.Domain.Users
{
	public record LoginInput(string UserName, string Password, bool RememberMe, string RecaptchaToken);
	public record CreateUserInput(string UserName, string Password, string EmailAddress, string FirstName, string LastName, string RecaptchaToken);
	public record VerifyUserInput(string emailAddress, string token);
	public record UpdateUserInput(string? FirstName, string? LastName, string? UserName, string ReCaptchaToken);
	public record ChangePasswordInput(string emailAddress, string verificationCode, string password, string RecaptchaToken);
}
