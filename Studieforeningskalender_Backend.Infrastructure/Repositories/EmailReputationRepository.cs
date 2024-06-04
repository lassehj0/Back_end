using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.EmailReputations;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class EmailReputationRepository : IEmailReputationRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<EmailReputation> _emailReputation;

		public EmailReputationRepository(AppDbContext context)
		{
			_context = context;
			_emailReputation = _context.EmailReputation;
		}

		public EmailReputation? GetEmailReputation(string emailAddress) =>
			_emailReputation.SingleOrDefault(e => e.EmailAddress == emailAddress);

		public void AddEmailReputation(EmailReputation emailReputation) =>
			_emailReputation.Add(emailReputation);

		public async Task<int> SaveChangesAsync() =>
			await _context.SaveChangesAsync();
	}
}
