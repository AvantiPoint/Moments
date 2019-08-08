using Newtonsoft.Json;

namespace Moments
{
	public class AccountRegistrationRequest
	{

        //--- Properties ---
		[JsonProperty("user")]
        public Account Account { get; set; }
		[JsonProperty("account")]
        public User User { get; set; }
	}

    public class AccountRegistrationResponse { }
}