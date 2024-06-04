using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IUserRepository
	{
		IQueryable<User> GetUser();
		IQueryable<User> GetUsers();
		User GetUserById([ID] Guid id);
		User? GetUserByUsername(string username);
		User? GetUserByEmailAddress(string emailAddress);
		bool UsernameIsInUse(string username);
		bool EmailIsInUse(string email);
		Task<int> CreateUser(User user);
		Task<int> DeleteUser(User user);
		Task<int> SaveChangesAsync();
	}
}