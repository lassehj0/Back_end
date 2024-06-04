using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.UserRoles;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class UserRoleRepository : IUserRoleRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<UserRole> _userRole;

		public UserRoleRepository(AppDbContext context)
		{
			_context = context;
			_userRole = _context.UserRole;
		}

		public IList<UserRole> GetRoleId(Guid id) =>
			_userRole.Where(u => u.UserId == id).ToList();

		public Guid? GetUserId(string username) =>
			_context.User.Where(u => u.UserName == username).FirstOrDefault()?.Id;

		public Guid? GetRoleId(string roleName) =>
			_context.Role.Where(u => u.Name == roleName).FirstOrDefault()?.Id;

		public async Task<int> GrantRoleToUser(UserRole userRole)
		{
			_userRole.Add(userRole);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> RemoveRoleFromUser(UserRole userRole)
		{
			_userRole.Remove(userRole);
			return await _context.SaveChangesAsync();
		}
	}
}