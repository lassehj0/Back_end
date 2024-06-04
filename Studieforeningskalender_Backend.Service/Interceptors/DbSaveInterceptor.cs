using Microsoft.EntityFrameworkCore.Diagnostics;
using Studieforeningskalender_Backend.Domain;

namespace Studieforeningskalender_Backend.Service.Interceptors;

public class DbSaveInterceptor : SaveChangesInterceptor
{
	public override void SaveChangesFailed(DbContextErrorEventData eventData)
	{
		// Handle error
		if (eventData.Exception != null)
		{
			throw new CustomDbException("A database operation failed.", eventData.Exception);
		}
	}

	public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
	{
		// Async handle error
		if (eventData.Exception != null)
		{
			throw new CustomDbException("A database operation failed.", eventData.Exception);
		}

		return Task.CompletedTask;
	}
}

