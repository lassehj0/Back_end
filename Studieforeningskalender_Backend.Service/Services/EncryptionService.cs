using Microsoft.Extensions.Configuration;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using System.Security.Cryptography;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class EncryptionService : IEncryptionService
	{
		private readonly byte[] _encryptionKey;
		private readonly byte[] _iv;

		public EncryptionService(IConfiguration configuration)
		{
			_encryptionKey = Convert.FromBase64String(configuration["Validation:Key"]);
			_iv = Convert.FromBase64String(configuration["Validation:IV"]);
		}

		public string Encrypt(string plainText)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = _encryptionKey;
				aesAlg.IV = _iv;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}

					return Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
		}

		public string Decrypt(string cipherText)
		{
			var buffer = Convert.FromBase64String(cipherText);

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = _encryptionKey;
				aesAlg.IV = _iv;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream msDecrypt = new MemoryStream(buffer))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}
	}
}
