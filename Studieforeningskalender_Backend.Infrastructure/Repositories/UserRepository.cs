using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DbSet<User> _user;
		private readonly AppDbContext _appDbContext;

		public UserRepository(AppDbContext context)
		{
			_appDbContext = context;
			_user = _appDbContext.User;
		}

		public IQueryable<User> GetUser() => _user;

		public IQueryable<User> GetUsers() => _user;

		public User? GetUserById([ID(null)] Guid id) => _user.Find(id);

		public User? GetUserByUsername(string username) =>
			_user.SingleOrDefault(user => user.UserName == username);

		public User? GetUserByEmailAddress(string emailAddress) =>
			_user.SingleOrDefault(x => x.EmailAddress == emailAddress);

		public bool UsernameIsInUse(string username) =>
			_user.Any(u => u.UserName == username);

		public bool EmailIsInUse(string email) =>
			_user.Any(u => u.EmailAddress == email);

		public async Task<int> CreateUser(User user)
		{
			_user.Add(user);
			return await _appDbContext.SaveChangesAsync();
		}

		public async Task<int> DeleteUser(User user)
		{
			_user.Remove(user);
			return await _appDbContext.SaveChangesAsync();
		}

		public async Task<int> SaveChangesAsync() =>
			await _appDbContext.SaveChangesAsync();
	}
}