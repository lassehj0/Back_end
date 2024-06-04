using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain;

namespace Studieforeningskalender_Backend.Service.Middleware;
public class ErrorHandlingMiddleware
{
	private readonly FieldDelegate _next;

	public ErrorHandlingMiddleware(FieldDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(IMiddlewareContext context)
	{
		try
		{
			await _next(context);
		}
		catch (CustomDbException ex)
		{
			// Check the exception details to customize the error message
			var message = "An error occurred while processing your request.";
			if (ex.InnerException is DbUpdateException)
			{
				var exceptionMsg = ex.InnerException.InnerException.Message;
				if (exceptionMsg.Contains("value too long for type character varying"))
					message = "An entered value is too long.";
				else
					message = $"A database update operation failed. \nException: {exceptionMsg}";
			}

			context.ReportError(ErrorBuilder.New()
				.SetMessage(message)
				.SetCode("DATABASE_ERROR")
				.SetException(ex.InnerException)
				.Build());
		}
	}
}
