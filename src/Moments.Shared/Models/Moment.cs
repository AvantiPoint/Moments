using System;
using Newtonsoft.Json;
using Humanizer;

namespace Moments
{
	public class Moment
	{
		public string Id { get; set; }

		[JsonProperty ("momentUrl")]
		public string MomentUrl { get; set; }

		[JsonProperty ("senderUserId")]
		public string SenderUserId { get; set; }

		[JsonProperty ("senderName")]
		public string SenderName { get; set; }

		[JsonProperty ("senderProfileImage")]
		public string SenderProfileImage { get; set; }

		[JsonProperty ("recipientUserId")]
		public string RecipientUserId { get; set; }

		[JsonProperty ("displayTime")]
		public int DisplayTime { get; set; }

		[JsonProperty ("timeSent")]
		public DateTime TimeSent { get; set; }

		[JsonIgnore]
		public string HumanizedTimeSent
		{
			get { return TimeSent.Humanize (false); }
		}
	}
}