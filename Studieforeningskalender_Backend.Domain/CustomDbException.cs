namespace Studieforeningskalender_Backend.Domain;
public class CustomDbException : Exception
{
	public CustomDbException() { }

	public CustomDbException(string message) : base(message) { }

	public CustomDbException(string message, Exception innerException) : base(message, innerException) { }
}
