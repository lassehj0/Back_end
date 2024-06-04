using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;
using System.Net;

namespace Studieforeningskalender_Backend.Service.Helpers
{
	public class HttpResponseFormatter : DefaultHttpResponseFormatter
	{
		protected override HttpStatusCode OnDetermineStatusCode(IQueryResult result,
			FormatInfo format, HttpStatusCode? proposedStatusCode)
		{
			// If an authorization error occurs respond with unauthorized
			if (result.Errors?.Count > 0 && result.Errors.Any(error =>
				error.Code == "AUTH_NOT_AUTHENTICATED" || error.Code == "AUTH_NOT_AUTHORIZED"))
			{
				return HttpStatusCode.Unauthorized;
			}

			// In all other cases let Hot Chocolate figure out the appropriate status code.
			return base.OnDetermineStatusCode(result, format, proposedStatusCode);
		}
	}

}
