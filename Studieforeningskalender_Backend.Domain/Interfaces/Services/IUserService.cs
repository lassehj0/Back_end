using Studieforeningskalender_Backend.Domain.Users;
using static Studieforeningskalender_Backend.Domain.Users.UserPayload;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IUserService
	{
		IQueryable<User> GetUserInfo();
		IQueryable<User> GetUsers();
		Task<CreateUserPayload> CreateUser(CreateUserInput createUserInput);
		Task<VerifyUserPayload> VerifyUser(string emailAddress, string token);
		Task<DeleteUserPayload> DeleteUser();
		Task<UpdateUserPayload> UpdateUser(UpdateUserInput updateUserInput);
		Task<ChangePasswordPayload> ChangePassword(string emailAddress, string verificationCode, string password, string reCaptcha);
		Task<LoginPayload> Login(LoginInput loginInput);
		Task SignOut();
	}
}