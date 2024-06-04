using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.Users;
using static Studieforeningskalender_Backend.Domain.Users.UserPayload;

namespace Studieforeningskalender_backend.Presentation.Mutation
{
	[ExtendObjectType(OperationTypeNames.Mutation)]
	public class UserMutation
	{
		public async Task<CreateUserPayload> CreateUser([Service] IUserService userService, CreateUserInput createUserInput) =>
			await userService.CreateUser(createUserInput);

		public async Task<VerifyUserPayload> VerifyUser([Service] IUserService userService, VerifyUserInput input) =>
			await userService.VerifyUser(input.emailAddress, input.token);

		public async Task<LoginPayload> Login([Service] IUserService userService, LoginInput loginInput) =>
			await userService.Login(loginInput);

		[Authorize]
		public async Task<string?> SignOut([Service] IUserService userService)
		{
			await userService.SignOut();
			return null;
		}

		public async Task<ChangePasswordPayload> ChangePassword([Service] IUserService service, ChangePasswordInput input) =>
			await service.ChangePassword(input.emailAddress, input.verificationCode, input.password, input.RecaptchaToken);

		[Authorize]
		public async Task<DeleteUserPayload> DeleteUser([Service] IUserService userService) =>
			await userService.DeleteUser();

		[Authorize]
		public async Task<UpdateUserPayload> UpdateUser([Service] IUserService userService, UpdateUserInput updateUserInput) =>
			await userService.UpdateUser(updateUserInput);
	}
}
