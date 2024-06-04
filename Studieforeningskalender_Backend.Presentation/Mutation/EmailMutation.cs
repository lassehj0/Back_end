using HotChocolate;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Emails;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;

namespace Studieforeningskalender_Backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class EmailMutation
	{
		public async Task<VerificationEmailPayload> SendForgotPasswordVerificationEmail([Service] IEmailService emailService, ForgotPasswordVerificationEmailInput input) =>
			await emailService.SendForgotPasswordVerificationEmail(input.email, input.reCaptchaToken);

		public async Task<VerificationEmailPayload> SendRegistrationVerificationEmail([Service] IEmailService emailService, RegisterVerificationEmailInput input) =>
			await emailService.SendRegisterVerificationEmail(input.email, input.userId, input.firstName);

		public async Task<VerificationEmailPayload> ResendRegistrationVerificationEmail([Service] IEmailService emailService) =>
			await emailService.ResendRegisterVerificationEmail();
	}
}
