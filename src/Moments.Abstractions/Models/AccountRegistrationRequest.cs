using Newtonsoft.Json;

namespace Moments
{
	public class AccountRegistrationRequest
	{

        //--- Properties ---
		[JsonProperty("account")]
        public Account Account { get; set; }

		[JsonProperty("user")]
        public User User { get; set; }
	}

    public class AccountRegistrationResponse { }
}