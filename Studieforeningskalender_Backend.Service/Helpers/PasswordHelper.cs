using System.Security.Cryptography;

namespace Studieforeningskalender_Backend.Service.Helpers
{
	public static class PasswordHelper
	{
		public static bool ValidatePasswordHash(string password, string dbPassword)
		{
			byte[] hashBytes = Convert.FromBase64String(dbPassword);

			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
			byte[] hash = pbkdf2.GetBytes(20);

			for (int i = 0; i < 20; i++)
				if (hashBytes[i + 16] != hash[i])
					return false;

			return true;
		}

		public static string HashPassword(string password)
		{
			byte[] salt = new byte[16];
			using (var rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(salt);
			}

			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000))
			{
				byte[] hash = pbkdf2.GetBytes(20); // Generate the hash

				// Combine the salt and hash to store
				byte[] hashBytes = new byte[36];
				Array.Copy(salt, 0, hashBytes, 0, 16);
				Array.Copy(hash, 0, hashBytes, 16, 20);

				// Convert to base64 for storage or comparison
				return Convert.ToBase64String(hashBytes);
			}
		}
	}
}
