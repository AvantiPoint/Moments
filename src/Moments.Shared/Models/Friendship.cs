using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moments
{
	public class Friendship
	{
		public string Id { get; set; }

		[JsonProperty ("userId")]
		public string UserId { get; set; }

		[JsonProperty ("friendId")]
		public string FriendId { get; set; }

		[JsonProperty ("accepted")]
		public bool Accepted { get; set; }
	}
}