using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SendWithBrevo;
using Studieforeningskalender_Backend.Domain.Emails;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Service.Helpers;
using System.Web;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class EmailService : IEmailService
	{
		private readonly IUserRepository _userRepository;
		private readonly IEncryptionService _encryptionService;
		private readonly IEmailReputationService _emailReputationService;
		private readonly string _baseUrl;
		private readonly BrevoClient _client;
		private readonly HttpContext? _context;
		private readonly IReCaptchaService _reCaptchaService;

		public EmailService(IUserRepository userRepository, IEncryptionService encryptionService, IEmailReputationService emailReputationService,
			IConfiguration configuration, IHttpContextAccessor contextAccessor, IReCaptchaService captchaService)
		{
			_userRepository = userRepository;
			_encryptionService = encryptionService;
			_emailReputationService = emailReputationService;
			_baseUrl = configuration["AllowedOrigin"];
			_client = new BrevoClient(configuration["Brevo:Key"]);
			_context = contextAccessor.HttpContext;
			_reCaptchaService = captchaService;
		}

		public async Task<VerificationEmailPayload> SendForgotPasswordVerificationEmail(string email, string reCaptchaToken)
		{
			var reCaptchaVerified = await _reCaptchaService.ValidateReCaptcha(reCaptchaToken);
			if (!reCaptchaVerified)
				return new VerificationEmailPayload(false, "The reCAPTCHA seems to think you are a robot, touch grass and try again");

			var (hasBadReputation, message) = _emailReputationService.CheckReputation(email);
			if (hasBadReputation)
				return new VerificationEmailPayload(false, message);

			var user = _userRepository
				.GetUsers()
				.Where(user => user.EmailAddress == email)
				.Select(user => new { user.Id, user.FirstName })
				.FirstOrDefault();

			if (user == null)
				return new VerificationEmailPayload(false, "No user with the specified email address could be found.");

			var plainText = user.Id + DateTime.UtcNow.AddHours(1).ToString();
			var verificationToken = _encryptionService.Encrypt(plainText);
			var urlToken = HttpUtility.UrlEncode(verificationToken);

			try
			{
				Console.WriteLine("Sending email using Brevo...");
				var response = await _client.SendAsync(
					new Sender("Studieforeningskalenderen", "noreply@studieforeningskalender.com"),
					new List<Recipient> { new Recipient(user.FirstName, email) },
					"Account verification",
					EmailHelper.GetForgotPasswordHTML(user.FirstName, email, urlToken, _baseUrl),
					true);

				if (!response)
					return new VerificationEmailPayload(false, "Could not connect to email service so no email has been sent, try again later");

				Console.WriteLine("The email was sent successfully.");

				if (message != null) return new VerificationEmailPayload(true, message);
				else return new VerificationEmailPayload(true);
			}
			catch (Exception ex)
			{
				Console.WriteLine("The email was not sent.");
				Console.WriteLine("Error message: " + ex.Message);

				return new VerificationEmailPayload(false, "Failed while trying to send the email, try again later.");
			}
		}

		public async Task<VerificationEmailPayload> SendRegisterVerificationEmail(string email, Guid userId, string firstName)
		{
			var (hasBadReputation, message) = _emailReputationService.CheckReputation(email);
			if (hasBadReputation)
				return new VerificationEmailPayload(false, message);

			var plainText = userId + DateTime.UtcNow.AddHours(1).ToString();
			var verificationToken = _encryptionService.Encrypt(plainText);
			var urlToken = HttpUtility.UrlEncode(verificationToken);

			try
			{
				Console.WriteLine("Sending email using Brevo...");
				var response = await _client.SendAsync(
					new Sender("Studieforeningskalenderen", "noreply@studieforeningskalender.com"),
					new List<Recipient> { new Recipient(firstName, email) },
					"Account verification",
					EmailHelper.GetVerifyRegistrationHTML(firstName, email, urlToken, _baseUrl),
					true);

				if (!response)
					return new VerificationEmailPayload(false, "Could not connect to email service so no email has been sent, try again later");

				Console.WriteLine("The email was sent successfully.");
				return new VerificationEmailPayload(true);
			}
			catch (Exception ex)
			{
				Console.WriteLine("The email was not sent.");
				Console.WriteLine("Error message: " + ex.Message);

				return new VerificationEmailPayload(false, "Failed while trying to send the email, try again later.");
			}
		}

		public async Task<VerificationEmailPayload> ResendRegisterVerificationEmail()
		{
			var username = _context?.User.Claims.First(c => c.Type == "UserName").Value;
			if (username == null)
				return new VerificationEmailPayload(false, "It seems that you are not logged in, log in and try again");

			var user = _userRepository
				.GetUsers()
				.Where(user => user.UserName == username)
				.Select(user => new { user.Id, user.FirstName, user.EmailAddress, isVerified = user.UserRoles.Any(x => x.Role.Name == "user") })
				.FirstOrDefault();

			if (user == null)
				return new VerificationEmailPayload(false, "It seems that not user exists with these credentials, try logging in or reregistering");
			if (user.isVerified)
				return new VerificationEmailPayload(false, "The user is already verified");

			var plainText = user.Id + DateTime.UtcNow.AddHours(1).ToString();
			var verificationToken = _encryptionService.Encrypt(plainText);

			try
			{
				Console.WriteLine("Sending email using Brevo...");
				var response = await _client.SendAsync(
					new Sender("Studieforeningskalenderen", "noreply@studieforeningskalender.com"),
					new List<Recipient> { new Recipient(user.FirstName, user.EmailAddress) },
					"Account verification",
					EmailHelper.GetVerifyRegistrationHTML(user.FirstName, user.EmailAddress, verificationToken, _baseUrl),
					true);

				if (!response)
					return new VerificationEmailPayload(false, "Could not connect to email service so no email has been sent, try again later");

				Console.WriteLine("The email was sent successfully.");
				return new VerificationEmailPayload(true);
			}
			catch (Exception ex)
			{
				Console.WriteLine("The email was not sent.");
				Console.WriteLine("Error message: " + ex.Message);

				return new VerificationEmailPayload(false, "Failed while trying to send the email, try again later.");
			}
		}
	}
}
