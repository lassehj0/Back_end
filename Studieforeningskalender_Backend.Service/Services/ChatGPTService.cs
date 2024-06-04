using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using System.Text;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class ChatGPTService : IChatGPTService
	{
		private readonly HttpClient _httpClient;

		public ChatGPTService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;

			_httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
			_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["ChatGPT:Key"]}");
		}

		public async Task<ChatGPTPayload> GetChatGPTDescription(string prompt)
		{
			var requestBody = new
			{
				model = "gpt-3.5-turbo",
				messages = new object[]
				{
					new { role = "system", content = "You are an assistant who helps the user create an engaging description for their event, you can include emojis. This will most likely be in danish but could also be in english. Always make the description less than 5000 tokens."},
					new { role = "user", content = prompt }
				}
			};

			var jsonContent = JsonConvert.SerializeObject(requestBody);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync("chat/completions", content);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				var chatResponse = JsonConvert.DeserializeObject<ChatCompletionResponse>(jsonResponse);

				if (chatResponse != null && chatResponse.Choices.Length > 0)
					return new ChatGPTPayload(true, chatResponse.Choices[0].Message.Content);
				else
					return new ChatGPTPayload(false, "", "There was an error with the content received from the AI assistant, try again with a different prompt.");
			}

			return new ChatGPTPayload(false, "", "An error occured while trying to get a response from the AI assistant");
		}
	}

	public class ChatCompletionResponse
	{
		public string Id { get; set; }
		public string Object { get; set; }
		public long Created { get; set; }
		public string Model { get; set; }
		public Choice[] Choices { get; set; }

		public class Choice
		{
			public int Index { get; set; }
			public Message Message { get; set; }
		}

		public class Message
		{
			public string Role { get; set; }
			public string Content { get; set; }
		}

		public class Usage
		{
			public int PromptTokens { get; set; }
			public int CompletionTokens { get; set; }
			public int TotalTokens { get; set; }
		}
	}
}
