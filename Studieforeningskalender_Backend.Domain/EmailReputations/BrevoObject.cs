using Newtonsoft.Json;

namespace Studieforeningskalender_Backend.Domain.EmailReputations
{
	public class BrevoObject
	{
		[JsonProperty("event")]
		public string? Event { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("id")]
		public int? Id { get; set; }

		[JsonProperty("date")]
		public string? Date { get; set; }

		[JsonProperty("ts")]
		public int? Timestamp { get; set; }

		[JsonProperty("message-id")]
		public string? MessageId { get; set; }

		[JsonProperty("ts_event")]
		public int? TimestampGMT { get; set; }

		[JsonProperty("subject")]
		public string? Subject { get; set; }

		[JsonProperty("X-Mailin-custom")]
		public string? CustomHeader { get; set; }

		[JsonProperty("sending_ip")]
		public string? SendingIp { get; set; }

		[JsonProperty("template_id")]
		public int? TemplateID { get; set; }

		[JsonProperty("tags")]
		public string[]? Tags { get; set; }

		[JsonProperty("tag")]
		public string? Tag { get; set; }

		[JsonProperty("ts_epoch")]
		public long? TsEpoch { get; set; }

		[JsonProperty("reason")]
		public string? Reason { get; set; }

	}
}
