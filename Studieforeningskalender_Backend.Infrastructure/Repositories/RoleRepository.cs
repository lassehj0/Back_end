using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Roles;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Role> _role;

		public RoleRepository(AppDbContext context)
		{
			_context = context;
			_role = _context.Role;
		}

		public IList<Role> GetRolesById(IList<Guid> roleIds) =>
			_role.Where(r => roleIds.Contains(r.Id)).ToList();

		public IQueryable<Role> GetRoles(IList<Guid>? roleIds)
		{
			var query = _role.AsQueryable();

			if (roleIds != null && roleIds.Any())
				query = query.Where(x => roleIds.Contains(x.Id));

			return query;
		}

		public async Task<int> CreateRole(Role newRole)
		{
			_role.Add(newRole);
			return await _context.SaveChangesAsync();
		}

		public Role GetRoleByName(string roleName) =>
			_role.Where(r => r.Name == roleName).FirstOrDefault();

		public async Task<int> DeleteRole(Role role)
		{
			_role.Remove(role);
			return await _context.SaveChangesAsync();
		}
	}
}
