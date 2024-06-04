namespace Studieforeningskalender_Backend.Domain.Users
{
	public class UserPayload
	{
		public record CreateUserPayload(bool isSuccessful, string message = "Created user successfully");
		public record DeleteUserPayload(bool isSuccessful, string message = "Deleted user successfully");
		public record UpdateUserPayload(bool isSuccessful, string message = "Updated user successfully");
		public record LoginPayload(bool isSuccessful, string message = "Logged in successfully");
		public record ChangePasswordPayload(bool isSuccessful, string message = "Password was changed successfully");
		public record VerifyUserPayload(bool isSuccessful, string message = "User was verified successfully");
	}
}