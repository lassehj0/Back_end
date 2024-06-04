namespace Studieforeningskalender_Backend.Service.ErrorFilters
{
	public class AuthErrorFilter : IErrorFilter
	{
		public IError OnError(IError error)
		{
			if (error.Code == "AUTH_NOT_AUTHENTICATED" || error.Code == "AUTH_NOT_AUTHORIZED")
			{
				return error.WithMessage("The user is not authorized to access this resource.")
					.RemoveLocations();
			}

			return error;
		}
	}

}
