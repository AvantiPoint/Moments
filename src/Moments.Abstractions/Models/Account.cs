using Newtonsoft.Json;

namespace Moments
{
	public class Account
	{
		public string Id { get; set; }

		[JsonProperty ("username"), JsonRequired]
		public string Username { get; set; }

		[JsonProperty ("password"), JsonRequired]
		public string Password { get; set; }

		[JsonProperty ("email")]
		public string Email { get; set; }

		[JsonProperty ("userId")]
		public string UserId { get; set; }
	}
}