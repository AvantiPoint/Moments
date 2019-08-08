using Newtonsoft.Json;

namespace Moments
{
	public class AccountRegistrationRequest
	{
        //--- Properties ---
		[JsonProperty("user")]
        Account Account { get; set; }
		[JsonProperty("account")]
        User User { get; set; }
	}

    public class AccountRegistrationResponse
    {

    }
}