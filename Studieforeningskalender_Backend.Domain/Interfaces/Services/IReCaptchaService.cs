namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IReCaptchaService
	{
		Task<bool> ValidateReCaptcha(string recaptcha);
	}
}
