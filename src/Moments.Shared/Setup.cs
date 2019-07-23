using System;
using System.Threading.Tasks;

namespace Moments
{
	public class Setup
	{
		public static async Task Init ()
		{
			if (AccountService.Instance.ReadyToSignIn) {
				await AccountService.Instance.Login ();
			}
		}
	}
}

