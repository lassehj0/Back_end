using Studieforeningskalender_Backend.Domain.EmailReputations;

namespace Studieforeningskalender_Backend.Domain.Interfaces.Repositories
{
	public interface IEmailReputationRepository
	{
		EmailReputation? GetEmailReputation(string emailAddress);
		void AddEmailReputation(EmailReputation emailReputation);
		Task<int> SaveChangesAsync();
	}
}
