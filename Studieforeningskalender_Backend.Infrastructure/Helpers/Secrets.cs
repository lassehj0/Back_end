using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace Studieforeningskalender_Backend.Infrastructure.Helpers
{
	public class Secrets
	{
		public static async Task<bool> SetSecrets(WebApplicationBuilder builder)
		{
			var client = new AmazonSimpleSystemsManagementClient(RegionEndpoint.EUNorth1);

			var request = new GetParametersRequest()
			{
				Names = new List<string>()
				{
					"connectionstring",
					"validation.key",
					"validation.iv",
					"Brevo.Key",
					"recaptcha.secret",
					"chatGPT.Key"
				}
			};
			request.WithDecryption = true;

			var result = new GetParametersResponse();

			try
			{
				Console.WriteLine("Fetching parameters from parameter store...");
				result = await client.GetParametersAsync(request);
				Console.WriteLine($"{result.Parameters.Count} parameters were successfully fetched from the parameter store");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			}

			builder.Configuration["ConnectionStrings:Postgres"] = result.Parameters.Where(x => x.Name == "connectionstring").First().Value;
			builder.Configuration["Validation:Key"] = result.Parameters.Where(x => x.Name == "validation.key").First().Value;
			builder.Configuration["Validation:IV"] = result.Parameters.Where(x => x.Name == "validation.iv").First().Value;
			builder.Configuration["Brevo:Key"] = result.Parameters.Where(x => x.Name == "Brevo.Key").First().Value;
			builder.Configuration["ReCAPTCHASecret"] = result.Parameters.Where(x => x.Name == "recaptcha.secret").First().Value;
			builder.Configuration["ChatGPT:Key"] = result.Parameters.Where(x => x.Name == "chatGPT.Key").First().Value;

			return true;
		}
	}
}
