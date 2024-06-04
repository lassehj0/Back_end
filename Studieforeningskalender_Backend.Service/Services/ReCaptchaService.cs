using Microsoft.Extensions.Configuration;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using System.Net.Http.Json;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class ReCaptchaService : IReCaptchaService
	{
		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
		public ReCaptchaService(IConfiguration configuration, HttpClient httpClient)
		{
			_configuration = configuration;
			_httpClient = httpClient;
		}

		public async Task<bool> ValidateReCaptcha(string recaptcha)
		{
			var reCaptchaSecretKey = _configuration["ReCAPTCHASecret"];

			if (string.IsNullOrEmpty(reCaptchaSecretKey) || string.IsNullOrEmpty(recaptcha))
				return false;

			var content = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"secret", reCaptchaSecretKey },
					{"response", recaptcha }
				});
			var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<reCaptchaResponse>();
				return result.Success;
			}

			return false;
		}

		public class reCaptchaResponse
		{
			public bool Success { get; set; }
			public string[] ErrorCodes { get; set; }
		}
	}
}
