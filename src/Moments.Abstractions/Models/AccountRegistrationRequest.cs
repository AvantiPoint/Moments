using Newtonsoft.Json;

namespace Moments
{
	public class AccountRegistrationRequest {

        //--- Properties ---
		[JsonProperty("account"), JsonRequired]
        public Account Account { get; set; }

		[JsonProperty("user")]
        public User User { get; set; }
	}

    public class AccountRegistrationResponse { }

	public class LoginRequest {

        //--- Properties ---
		[JsonProperty("account"), JsonRequired]
        public Account Account { get; set; }
	}

	public class LoginResponse {

        //--- Properties ---
		[JsonProperty("sessionToken")]
		public string SessionToken { get; set; }
	}
}