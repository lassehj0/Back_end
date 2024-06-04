namespace Studieforeningskalender_Backend.Domain.Interfaces.Services
{
	public interface IEncryptionService
	{
		string Encrypt(string plainText);
		string Decrypt(string cipherText);
	}
}
